using Serilog;
using SMS.Application;
using SMS.Infrastructure;
using SMS.Presentation.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.RegisterEndpoints();

app.Run();
