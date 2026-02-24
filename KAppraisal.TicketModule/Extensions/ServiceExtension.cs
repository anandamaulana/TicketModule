using KAppraisal.TicketModule.Contexts;
using KAppraisal.TicketModule.Repositories;
using KAppraisal.TicketModule.Services;
using Microsoft.EntityFrameworkCore;

namespace KAppraisal.TicketModule.Extensions;

public static class ServiceExtension
{
    public static void AddLocalServices(this IServiceCollection services)
    {
        services.AddTransient<DbContext, TicketDbContext>();
        services.AddTransient<TicketDbContext>();

        services.AddTransient(typeof(IRootRepository<>), typeof(RootRepository<>));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        services.AddTransient<ITicketService, TicketService>();
        services.AddTransient<IUserService, UserService>();

        services.AddHttpContextAccessor();
    }
}
