using Serilog;
using SMS.API.Extensions;
using SMS.API.Middleware;
using SMS.Application;
using SMS.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddApiServices();

builder.Services.AddSerilog();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.SeedDatabase();

app.UseSerilogRequestLogging();

app.RegisterEndpoints();

app.Run();
