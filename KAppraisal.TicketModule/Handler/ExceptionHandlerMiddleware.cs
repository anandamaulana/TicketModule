using KAppraisal.TicketModule.Exceptions;
using KAppraisal.TicketModule.ViewModels;
using System.Text.Json;

namespace KAppraisal.TicketModule.Handler;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = (int)ex.Type;
            context.Response.ContentType = "application/json";
            var response = new ExceptionDto
            {
                StatusCode = (int)ex.Type,
                Message = ex.BuildMessage(ex.ExceptionMessage),
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            var response = new ExceptionDto
            {
                StatusCode = 500,
                Message = ex.Message,
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
