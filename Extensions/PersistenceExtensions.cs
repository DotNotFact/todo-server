using ToDoServer.Repositories.Abstracts;
using ToDoServer.Repositories.Bases;
using Microsoft.EntityFrameworkCore;
using ToDoServer.Services.Abstracts;
using ToDoServer.Services.Bases;
using ToDoServer.Data;

namespace ToDoServer.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionStringOrThrow(nameof(ToDoContext))));

        services
            .AddScoped<ITasksRepository, TasksRepository>();

        services
            .AddScoped<IToDoService, ToDoService>();

        services.AddControllers();
        services.AddOpenApi();

        return services;
    }
}
