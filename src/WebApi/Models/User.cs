namespace WebApi.Models;

public class User
{
    public User(
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

    public ICollection<TodoItem> TodoItems { get; set; } = [];
}
