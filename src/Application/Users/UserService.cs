using Domain.Common.Interfaces;
using Domain.Users;

namespace Application.Users;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public async Task<IEnumerable<UserEntity>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        return await unitOfWork.Repository<UserEntity>().GetAllAsync(cancellationToken);
    }
    
    public async Task<UserEntity?> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await unitOfWork.Repository<UserEntity>().GetByIdAsync(id, cancellationToken);
    }

    public async Task<UserEntity> CreateUserAsync(CreateUser createUser)
    {
        UserEntity userEntity = new(createUser.UserName, createUser.Email);
        
        await unitOfWork.Repository<UserEntity>().AddAsync(userEntity);
        await unitOfWork.SaveChangesAsync();
        return userEntity;
    }

    public async Task UpdateUserAsync(UserEntity updatedUserEntity)
    {
        unitOfWork.Repository<UserEntity>().Update(updatedUserEntity);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(UserEntity userEntity)
    {
        unitOfWork.Repository<UserEntity>().Remove(userEntity);
        await unitOfWork.SaveChangesAsync();
    }
}