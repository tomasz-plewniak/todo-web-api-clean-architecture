using Application.Users.Mappers;
using Contracts.Common.TodoItems;
using Contracts.Responses.TodoItems;
using Domain.TodoItems;

namespace Application.TodoItems.Mappers;

public static class TodoItemMapper
{
    public static TodoItemDto ToDto(this TodoItemEntity entity)
    {
        return new TodoItemDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Completed = entity.Completed,
            DueDate = entity.DueDate,
            Priority = entity.Priority?.ToDto(),
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            UserId = entity.UserId,
            User = entity.UserEntity?.ToDto()
        };
    }

    public static IEnumerable<TodoItemDto> ToDto(this IEnumerable<TodoItemEntity> entities)
    {
        return entities.Select(ToDto);
    }

    private static PriorityDto? ToDto(this Priority priority)
    {
        return priority switch
        {
            Priority.Low => PriorityDto.Low,
            Priority.Medium => PriorityDto.Medium,
            Priority.High => PriorityDto.High,
            _ => null
        };
    }
}