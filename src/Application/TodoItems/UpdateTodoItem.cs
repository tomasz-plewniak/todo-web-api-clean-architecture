using System.ComponentModel.DataAnnotations;
using Domain.TodoItems;

namespace Application.TodoItems;

public record UpdateTodoItem(
    [MaxLength(100)]
    string? Title,
    [MaxLength(1000)]
    string? Description,
    DateTime? DueDate,
    Priority? Priority);