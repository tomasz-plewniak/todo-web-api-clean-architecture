using Domain.Users;

namespace Infrastructure.Data.Repositories;

public class UserRepository(AppDbContext context)
    : Repository<UserEntity>(context), IUserRepository
{
}