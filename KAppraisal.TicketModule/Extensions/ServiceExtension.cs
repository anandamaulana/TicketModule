using Amazon.S3;
using Amazon.S3.Model;
using KAppraisal.TicketModule.Clients;
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

        services.AddTransient<IFileSystemClient, FileSystemClient>();

        services.AddHttpContextAccessor();
        services.AddTransient<IAmazonS3>(provider =>
{
    var config = new AmazonS3Config
    {
        ServiceURL = "http://127.0.0.1:9000",
        ForcePathStyle = true,
        UseHttp = true
    };

    return new AmazonS3Client(
        "minioadmin",
        "minioadmin",
        config
    );
});

    }
}
