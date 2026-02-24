using Microsoft.AspNetCore.Mvc;

namespace KAppraisal.TicketModule.Extensions;

public static class ControllerExtension
{
    public static void AddLocalControllers(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });
    }
}
