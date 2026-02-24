using KAppraisal.TicketModule.Handler;

namespace KAppraisal.TicketModule.Extensions;

public static class MiddlewareExtension
{
    public static void UseMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
