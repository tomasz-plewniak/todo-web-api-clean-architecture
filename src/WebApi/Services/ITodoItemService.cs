using WebApi.Models;

namespace WebApi.Services;

public interface ITodoItemService
{
    public Task<IEnumerable<TodoItem>> GetTodoItemsAsync();
    
    public Task<TodoItem?> GetTodoItemAsync(Guid id);
    
    public Task CreateTodoItemAsync(TodoItem todoItem);
    
    public Task UpdateTodoItemAsync(
        UpdateTodoItem updateTodoItem,
        TodoItem todoItem);
    
    public Task DeleteTodoItemAsync(TodoItem todoItem);
}