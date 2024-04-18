using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SMS.API.IntegrationTests.Utils;
using SMS.Infrastructure.Data;
using Testcontainers.PostgreSql;

namespace SMS.API.IntegrationTests.Fixtures;
public class CustomApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveDbContext<SchoolDbContext>();

            services.AddDbContext<SchoolDbContext>(opt =>
            {
                opt.UseNpgsql(_container.GetConnectionString());
            });

            services.DbContextInit<SchoolDbContext>();
        });
    }

    public new async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
