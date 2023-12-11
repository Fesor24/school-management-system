using MediatR;
using SMS.API.Abstractions;
using SMS.API.Extensions;
using SMS.Application.Courses.Commands.CreateCourse;

namespace SMS.API.EndpointDefinitions;
internal class CourseEndpoint : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        const string ENDPOINT = "course";

        app.MediatorPost<CreateCourseCommand, Unit>(ENDPOINT, "/");
    }
}
