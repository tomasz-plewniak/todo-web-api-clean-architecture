using Domain.Users;

namespace Domain.TodoItems;

public class TodoItemEntity
{
    public TodoItemEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }
    
    public required string Title { get; set; }

    public string? Description { get; set; }

    public bool Completed { get; set; }
    
    public DateTime? DueDate { get; set; }

    public Priority? Priority { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public Guid UserId { get; set; }
    
    public UserEntity? UserEntity { get; set; }
}
