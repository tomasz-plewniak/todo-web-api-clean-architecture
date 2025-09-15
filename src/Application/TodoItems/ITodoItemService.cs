using Domain.TodoItems;

namespace Application.TodoItems;

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