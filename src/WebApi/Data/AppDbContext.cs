using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<User> Users { get; set; }
}