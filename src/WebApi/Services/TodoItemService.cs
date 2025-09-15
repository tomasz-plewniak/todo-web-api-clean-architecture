using Domain.TodoItems;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services;

public class TodoItemService : ITodoItemService
{
    private readonly AppDbContext _context;

    public TodoItemService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TodoItemEntity>> GetTodoItemsAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItemEntity?> GetTodoItemAsync(Guid id)
    {
        return await _context.TodoItems.SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateTodoItemAsync(TodoItemEntity todoItemEntity)
    {
        await _context.TodoItems.AddAsync(todoItemEntity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoItemAsync(UpdateTodoItem updateTodoItem, TodoItemEntity todoItemEntity)
    {
        if (updateTodoItem.Title is not null)
        {
            todoItemEntity.Title = updateTodoItem.Title;
        }
        
        if (updateTodoItem.Description is not null)
        {
            todoItemEntity.Description = updateTodoItem.Description;
        }

        if (updateTodoItem.DueDate is not null)
        {
            todoItemEntity.DueDate = updateTodoItem.DueDate;
        }

        if (updateTodoItem.Priority is not null)
        {
            todoItemEntity.Priority = updateTodoItem.Priority;
        }

        _context.TodoItems.Update(todoItemEntity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoItemAsync(TodoItemEntity todoItemEntity)
    {
        _context.TodoItems.Remove(todoItemEntity);
        await _context.SaveChangesAsync();
    }
}