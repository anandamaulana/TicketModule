using KAppraisal.TicketModule.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase();
builder.Services.AddLocalServices();
builder.Services.AddMapperConfiguration();
builder.Services.AddLocalControllers();
builder.Services.AddLocalOpenApiDocument();
builder.Services.AddLocalCors();
builder.Services.AddHttpClients();

var app = builder.Build();

app.UseLocalStaticFiles(); 
app.UseLocalCors();
app.UseOpenApi();
app.UseDatabase();
app.MapControllers();
app.UseSwaggerUi();
app.UseMiddlewares();
app.UseLocalCors();

app.Run();
