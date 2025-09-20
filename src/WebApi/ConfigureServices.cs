using System.Diagnostics.CodeAnalysis;
using Application.TodoItems;
using Application.Users;
using WebApi.Services;

namespace WebApi;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(
        this IServiceCollection services)
    {
        services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}