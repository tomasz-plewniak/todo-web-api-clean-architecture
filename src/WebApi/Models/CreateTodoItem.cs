using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public record CreateTodoItem
{
    [MaxLength(100)]
    public required string Title { get; init; }

    [MaxLength(1000)]
    public string? Description { get; init; }
    
    public DateTime? DueDate { get; init; }

    public Priority? Priority { get; init; }

    [Required]
    public Guid UserId { get; init; }
}