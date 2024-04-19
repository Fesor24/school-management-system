using Bogus;
using FluentAssertions;
using SMS.API.IntegrationTests.Fixtures;
using SMS.Application.Department.Commands.CreateDepartment;
using SMS.Application.Department.Response;
using System.Net.Http.Json;

namespace SMS.API.IntegrationTests.EndpointTests.DepartmentEndpointTests;

[Collection("Api Collection")]
public class DeleteDepartmentEndpointTests
{
    private readonly HttpClient _httpClient;

    public DeleteDepartmentEndpointTests(CustomApplicationFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Delete_DeleteDepartment_WhenDataExist()
    {
        CreateDepartmentCommand department =
            new("Social Sciences", "SSC", new List<Application.Courses.Request.CreateCourseRequest>());

        var createResponse = await _httpClient.PostAsJsonAsync("api/department", department);

        createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var deptCreated = await createResponse.Content.ReadFromJsonAsync<CreateDepartmentResponse>();

        var response = await _httpClient.DeleteAsync($"api/department/{deptCreated.Id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteDepartment_ReturnNotFound_WhenDataDoesNotExist()
    {
        Guid departmentId = Guid.NewGuid();

        var response = await _httpClient.DeleteAsync($"api/department/{departmentId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
