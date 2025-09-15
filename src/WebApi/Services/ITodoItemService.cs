using Domain.TodoItems;
using WebApi.Models;

namespace WebApi.Services;

public interface ITodoItemService
{
    public Task<IEnumerable<TodoItemEntity>> GetTodoItemsAsync();
    
    public Task<TodoItemEntity?> GetTodoItemAsync(Guid id);
    
    public Task CreateTodoItemAsync(TodoItemEntity todoItemEntity);
    
    public Task UpdateTodoItemAsync(
        UpdateTodoItem updateTodoItem,
        TodoItemEntity todoItemEntity);
    
    public Task DeleteTodoItemAsync(TodoItemEntity todoItemEntity);
}