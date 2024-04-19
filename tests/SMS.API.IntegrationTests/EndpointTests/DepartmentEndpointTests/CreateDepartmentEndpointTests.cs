using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using SMS.API.IntegrationTests.Fixtures;
using SMS.Application.Department.Commands.CreateDepartment;
using SMS.Application.Department.Response;
using System.Net.Http.Json;

namespace SMS.API.IntegrationTests.EndpointTests.DepartmentEndpointTests;

[Collection("Api Collection")]
public class CreateDepartmentEndpointTests
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

    [Fact]
    public async Task Create_ReturnsValidationError_WhenDataIsNotValid()
    {
        CreateDepartmentCommand department =
            new("", "SSC", new List<Application.Courses.Request.CreateCourseRequest>());

        var response = await _httpClient.PostAsJsonAsync("api/department", department);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.UnprocessableEntity);

        var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        error.Title.Should().Be("One or more validation error(s)");

        error.Detail.Should().Be("{\"Name\":[\"Name can not be empty\"]}");
    }
}
