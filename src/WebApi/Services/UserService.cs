using Application.Users;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _appDbContext;

    public UserService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<UserEntity>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.ToListAsync(cancellationToken);
    }
    
    public async Task<UserEntity?> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<UserEntity> CreateUserAsync(CreateUser createUser)
    {
        UserEntity userEntity = new(createUser.UserName, createUser.Email);
        
        _appDbContext.Users.Add(userEntity);
        await _appDbContext.SaveChangesAsync();
        return userEntity;
    }

    public async Task UpdateUserAsync(UserEntity updatedUserEntity)
    {
        _appDbContext.Users.Update(updatedUserEntity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(UserEntity userEntity)
    {
        _appDbContext.Users.Remove(userEntity);
        await _appDbContext.SaveChangesAsync();
    }
}