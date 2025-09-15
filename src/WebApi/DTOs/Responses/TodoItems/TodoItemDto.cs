using WebApi.DTOs.Common.TodoItems;
using WebApi.DTOs.Responses.Users;

namespace WebApi.DTOs.Responses.TodoItems;

public record TodoItemDto
{
    public Guid Id { get; init; }
    
    public required string Title { get; init; }

    public string? Description { get; init; }

    public bool Completed { get; init; }
    
    public DateTime? DueDate { get; init; }

    public PriorityDto? Priority { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime? UpdatedAt { get; init; }
    
    public Guid UserId { get; init; }
    
    public UserDto? User { get; init; }
}