using Domain.TodoItems;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/todo-items")]
public class TodoItemsController : ControllerBase
{
    private readonly ITodoItemService _todoItemService;

    public TodoItemsController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }
    
    [HttpGet]
    public async Task<IActionResult>  GetTodoItemsAsync()
    {
        return Ok(await _todoItemService.GetTodoItemsAsync());
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetTodoItemAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodoItemAsync(Guid id)
    {
        TodoItemEntity? todoItem = await _todoItemService.GetTodoItemAsync(id);
        
        if (todoItem == null)
        {
            return NotFound();
        }
        
        return Ok(todoItem);
    }

    [HttpPost]
    [ActionName(nameof(CreateTodoItemAsync))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodoItemAsync(CreateTodoItem createTodoItem)
    {
        TodoItemEntity todoItemEntity = new()
        {
            Title = createTodoItem.Title,
            Description = createTodoItem.Description,
            DueDate = createTodoItem.DueDate,
            Priority = createTodoItem.Priority,
            UserId = createTodoItem.UserId
        };

        await _todoItemService.CreateTodoItemAsync(todoItemEntity);
        
        return CreatedAtAction(
            nameof(GetTodoItemAsync),
            new {id = todoItemEntity.Id},
            todoItemEntity); 
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateTodoItemAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateTodoItem updateTodoItem)
    { 
        TodoItemEntity? todoItem = await _todoItemService.GetTodoItemAsync(id);
        
        if (todoItem == null)
        {
            return NotFound();
        }
        
        await _todoItemService.UpdateTodoItemAsync(updateTodoItem, todoItem);
        
        return Ok(todoItem);
    }
    
    [HttpDelete("{id}")]
    [ActionName(nameof(DeleteTodoItemAsync))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodoItemAsync(Guid id)
    {
        TodoItemEntity? todoItem = await _todoItemService.GetTodoItemAsync(id);
        
        if (todoItem == null)
        {
            return NotFound();
        }
        
        await _todoItemService.DeleteTodoItemAsync(todoItem);
        
        return NoContent();
    }
}