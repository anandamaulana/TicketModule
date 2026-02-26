namespace KAppraisal.TicketModule.Extensions;

public static class StaticFilesExtension
{
    public static void UseLocalStaticFiles(this WebApplication app)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "uploads")
            ),
            RequestPath = "/uploads"
        });
    }
}