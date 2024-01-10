using Serilog;
using SMS.API.Extensions;
using SMS.API.Middleware;
using SMS.Application;
using SMS.Infrastructure;
using SMS.Infrastructure.Data.DataSeed;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

builder.Services.AddSerilog();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSerilogRequestLogging();

app.RegisterEndpoints();

app.Run();
