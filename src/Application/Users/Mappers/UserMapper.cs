using Contracts.Responses.Users;
using Domain.Users;

namespace Application.Users.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this UserEntity entity)
    {
        return new UserDto
        {
            Id = entity.Id,
            UserName = entity.UserName,
            Email = entity.Email,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static IEnumerable<UserDto> ToDto(this IEnumerable<UserEntity> entities)
    {
        return entities.Select(ToDto);
    }
}