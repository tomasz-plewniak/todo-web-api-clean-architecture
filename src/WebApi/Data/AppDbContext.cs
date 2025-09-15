using Domain.TodoItems;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TodoItemEntity> TodoItems { get; set; }

    public DbSet<UserEntity> Users { get; set; }
}