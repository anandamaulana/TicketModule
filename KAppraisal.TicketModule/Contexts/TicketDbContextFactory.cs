using DotNetEnv;
using KAppraisal.TicketModule.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KAppraisal.TicketModule.Contexts;

public class TicketDbContextFactory : IDesignTimeDbContextFactory<TicketDbContext>
{
    public TicketDbContext CreateDbContext(string[] args)
    {
        Env.Load();
        var connectionString = Env.GetString(
            EnvironmentConstants.DbConnectionString,
            "Host=localhost;Port=5432;Database=ticket_db;Username=postgres;Password=postgres"
        );

        var optionsBuilder = new DbContextOptionsBuilder<TicketDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new TicketDbContext(optionsBuilder.Options);
    }
}