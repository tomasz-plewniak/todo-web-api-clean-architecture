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
    
    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }

    public async Task<TodoItem?> GetTodoItemAsync(Guid id)
    {
        return await _context.TodoItems.SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateTodoItemAsync(TodoItem todoItem)
    {
        await _context.TodoItems.AddAsync(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTodoItemAsync(UpdateTodoItem updateTodoItem, TodoItem todoItem)
    {
        if (updateTodoItem.Title is not null)
        {
            todoItem.Title = updateTodoItem.Title;
        }
        
        if (updateTodoItem.Description is not null)
        {
            todoItem.Description = updateTodoItem.Description;
        }

        if (updateTodoItem.DueDate is not null)
        {
            todoItem.DueDate = updateTodoItem.DueDate;
        }

        if (updateTodoItem.Priority is not null)
        {
            todoItem.Priority = updateTodoItem.Priority;
        }

        _context.TodoItems.Update(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTodoItemAsync(TodoItem todoItem)
    {
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
    }
}