using KAppraisal.TicketModule.Extensions;
using KAppraisal.TicketModule.Clients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase();
builder.Services.AddLocalServices();
builder.Services.AddMapperConfiguration();
builder.Services.AddLocalControllers();
builder.Services.AddLocalOpenApiDocument();
builder.Services.AddLocalCors();
builder.Services.AddScoped<IFileSystemClient, FileSystemClient>();
// builder.Services.AddSwaggerWithUserIdHeader();

var app = builder.Build();

app.UseLocalCors();
app.UseOpenApi();
app.UseDatabase();
app.MapControllers();
app.UseSwaggerUi();
app.UseMiddlewares();
app.UseLocalCors();
app.UseDeveloperExceptionPage();

app.Run();
