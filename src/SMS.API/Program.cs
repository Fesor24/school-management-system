using SMS.Application;
using SMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
