using Domain.TodoItems;

namespace Domain.Users;

public class UserEntity
{
    public UserEntity(
        string userName,
        string email)
    {
        UserName = userName;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }
    
    public Guid Id { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }

    public ICollection<TodoItemEntity> TodoItems { get; set; } = [];
}
