using WebApi.DTOs.Responses.TodoItems;

namespace WebApi.DTOs.Responses.Users;

public record UserDto
{
    public Guid Id { get; init; }
    
    public required string UserName { get; init; }
    
    public required string Email { get; init; }

    public DateTime CreatedAt { get; init; }
    
    public DateTime? UpdatedAt { get; init; }

    public ICollection<TodoItemDto> TodoItems { get; set; } = [];
}