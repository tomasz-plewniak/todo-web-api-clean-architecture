using Domain.Users;
using WebApi.Models;

namespace WebApi.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default);
    
    Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<User> CreateUserAsync(CreateUser createUser);
    
    Task UpdateUserAsync(User updatedUser);
    
    Task DeleteUserAsync(User user);
}