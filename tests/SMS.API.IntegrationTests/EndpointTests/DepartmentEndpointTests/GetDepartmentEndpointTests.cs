using FluentAssertions;
using SMS.API.IntegrationTests.Fixtures;
using SMS.Application.Department.Commands.CreateDepartment;
using SMS.Application.Department.Response;
using System.Net.Http.Json;

namespace SMS.API.IntegrationTests.EndpointTests.DepartmentEndpointTests;

[Collection("Api Collection")]
public class GetDepartmentEndpointTests
{
    private readonly HttpClient _httpClient;

    public GetDepartmentEndpointTests(CustomApplicationFactory appFactory)
    {
        _httpClient = appFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnDepartment_WhenDepartmentExist()
    {
        CreateDepartmentCommand dept =
            new("Law", "LW", new List<Application.Courses.Request.CreateCourseRequest>());

        var createResponse = await _httpClient.PostAsJsonAsync("api/department", dept);

        createResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        var departmentCreated = await createResponse.Content.ReadFromJsonAsync<CreateDepartmentResponse>();

        var response = await _httpClient.GetAsync($"/api/department/{departmentCreated.Id}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        response.Should().NotBeNull();

        var department = await response.Content.ReadFromJsonAsync<GetDepartmentResponse>();

        department!.Id.Should().Be(departmentCreated.Id);
    }

    [Fact]
    public async Task Get_ReturnNotFound_WhenDepartmentDoesNotExist()
    {
        Guid departmentId = Guid.Parse("1680F9D6-C6D5-4648-A614-8EF46E073692");

        var response = await _httpClient.GetAsync($"/api/department/{departmentId}");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Get_ReturnsListOfDepartment_WhenDepartmentsExists()
    {
        var response = await _httpClient.GetFromJsonAsync<List<GetDepartmentResponse>>("api/department");

        response.Should().NotBeNullOrEmpty();

        response.Capacity.Should().BeGreaterThan(1);

        response.ForEach(dept =>
        {
            dept.Code.Should().NotBeNullOrWhiteSpace();

            dept.Name.Should().NotBeNullOrWhiteSpace();
        });
    }
}
