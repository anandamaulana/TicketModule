using KAppraisal.TicketModule.Exceptions;

namespace KAppraisal.TicketModule.Extensions;

public static class ExceptionExtension
{
    public static void ConfigureExceptionHandling(this IServiceCollection services)
    {
        // services.AddExceptionHandler<AppException>();
    }
}
