using DotNetEnv;
using KAppraisal.TicketModule.Contexts;
using KAppraisal.TicketModule.Helpers;
using Microsoft.EntityFrameworkCore;

namespace KAppraisal.TicketModule.Extensions;

public static class DbExtension
{
    public static void AddDatabase(this IServiceCollection services)
    {
        Env.Load();
        var connectionString = Env.GetString(
            EnvironmentConstants.DbConnectionString,
            "Host=localhost;Port=5432;Database=ticket_db;Username=postgres;Password=postgres"
        );
        services.AddDbContext<TicketDbContext>(opt =>
        {
            opt.UseNpgsql(connectionString);
        });
    }

    public static void UseDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
        
        context.Database.Migrate();
    }
}
 