using Microsoft.EntityFrameworkCore;
using ToDoServer.Data;

namespace ToDoServer.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ToDoContext>();

        context.Database.Migrate();
    }
}
