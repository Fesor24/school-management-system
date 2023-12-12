using SMS.API.Abstractions;
using SMS.API.Extensions;
using SMS.Application.Courses.Commands.CreateCourse;
using SMS.Application.Courses.Response;
using SMS.Domain.Shared;

namespace SMS.API.EndpointDefinitions;
internal class CourseEndpoint : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        const string ENDPOINT = "course";

        app.MediatorPost<CreateCourseCommand, Result<CreateCourseResponse, Error>, CreateCourseResponse, Error>(
            ENDPOINT, "/");
    }
}
