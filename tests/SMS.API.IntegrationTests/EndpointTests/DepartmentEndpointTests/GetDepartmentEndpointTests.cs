using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SMS.Application.Department.Response;
using System.Net.Http.Json;

namespace SMS.API.IntegrationTests.EndpointTests.DepartmentEndpointTests;
public class GetDepartmentEndpointTests : IClassFixture<WebApplicationFactory<IApiMarker>>
{
    private readonly HttpClient _httpClient;

    public GetDepartmentEndpointTests(WebApplicationFactory<IApiMarker> appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Theory]
    [InlineData("f002392c-70e1-42dd-a3e9-467ee9c42284")]
    public async Task Get_ReturnDepartment_WhenDepartmentExist(Guid departmentId)
    {
        var response = await _httpClient.GetAsync($"/api/department/{departmentId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        response.Should().NotBeNull();

        var department = await response.Content.ReadFromJsonAsync<GetDepartmentResponse>();

        department!.Id.Should().Be(departmentId);
    }

    [Fact]
    public async Task Get_ReturnNotFound_WhenDepartmentDoesNotExist()
    {
        Guid departmentId = Guid.Parse("1680F9D6-C6D5-4648-A614-8EF46E073692");

        var response = await _httpClient.GetAsync($"/api/department/{departmentId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
