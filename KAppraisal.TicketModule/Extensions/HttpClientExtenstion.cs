using DotNetEnv;
using KAppraisal.TicketModule.Clients;
using KAppraisal.TicketModule.Helpers;

namespace KAppraisal.TicketModule.Extensions;

public static class HttpClientExtension
{
    public static void AddHttpClients(this IServiceCollection services)
    {
        Env.Load();

        services.AddHttpClient<IFileSystemClient, FileSystemClient>(client =>
        {
            var url = Env.GetString(EnvironmentConstants.FileSystemUrl, "http://localhost:5100");
            client.BaseAddress = new Uri(url);
        });
    }
}