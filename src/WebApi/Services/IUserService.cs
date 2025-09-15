using Domain.Users;
using WebApi.Models;

namespace WebApi.Services;

public interface IUserService
{
    Task<IEnumerable<UserEntity>> GetUsersAsync(CancellationToken cancellationToken = default);
    
    Task<UserEntity?> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<UserEntity> CreateUserAsync(CreateUser createUser);
    
    Task UpdateUserAsync(UserEntity updatedUserEntity);
    
    Task DeleteUserAsync(UserEntity userEntity);
}