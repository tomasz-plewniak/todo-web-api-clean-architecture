using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _appDbContext;

    public UserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.ToListAsync(cancellationToken);
    }
    
    public async Task<User?> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User> CreateUserAsync(CreateUser createUser)
    {
        User user = new(createUser.UserName, createUser.Email);
        
        _appDbContext.Users.Add(user);
        await _appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUserAsync(User updatedUser)
    {
        _appDbContext.Users.Update(updatedUser);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync();
    }
}