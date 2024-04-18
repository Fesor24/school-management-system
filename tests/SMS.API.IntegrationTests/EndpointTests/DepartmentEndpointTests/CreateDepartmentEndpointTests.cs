using FluentAssertions;
using SMS.API.IntegrationTests.Fixtures;
using SMS.Application.Department.Commands.CreateDepartment;
using SMS.Application.Department.Response;
using System.Net.Http.Json;

namespace SMS.API.IntegrationTests.EndpointTests.DepartmentEndpointTests;
public class CreateDepartmentEndpointTests : IClassFixture<CustomApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public CreateDepartmentEndpointTests(CustomApplicationFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Create_CreateDepartment_WhenDataIsValid()
    {
        CreateDepartmentCommand department = 
            new("Social Sciences", "SSC", new List<Application.Courses.Request.CreateCourseRequest>());

        var response = await _httpClient.PostAsJsonAsync("api/department", department);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var deptCreated = await response.Content.ReadFromJsonAsync<CreateDepartmentResponse>();

        response.Headers.Location.Should().Be($"http://localhost/api/department/{deptCreated.Id}");

        deptCreated!.Code.Should().Be(department.Code);
    }
}
