using NSwag.AspNetCore;

namespace KAppraisal.TicketModule.Extensions;

public static class ApiDocumentExtension
{
    public static void AddLocalOpenApiDocument(this IServiceCollection services)
    {
        services.AddOpenApiDocument(config =>
        {
            config.Title = "KAppraisal Ticket Module API";
            config.Version = "v1";
            config.Description = "API for KAppraisal Ticket System";
        });
    }
}
