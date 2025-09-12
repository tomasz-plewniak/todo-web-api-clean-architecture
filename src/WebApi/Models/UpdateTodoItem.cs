using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public record UpdateTodoItem(
    [MaxLength(100)]
    string? Title,
    [MaxLength(1000)]
    string? Description,
    DateTime? DueDate,
    Priority? Priority);