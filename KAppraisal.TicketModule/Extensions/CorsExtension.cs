namespace KAppraisal.TicketModule.Extensions;

public static class CorsExtension
{
    public static void AddLocalCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
    }

    public static void UseLocalCors(this WebApplication app)
    {
        app.UseCors();
    }
}