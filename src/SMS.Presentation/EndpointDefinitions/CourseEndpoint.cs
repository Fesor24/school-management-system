using Microsoft.AspNetCore.Builder;
using SMS.Application.Courses.Commands.CreateCourse;
using SMS.Presentation.Abstractions;
using SMS.Presentation.Extensions;

namespace SMS.Presentation.EndpointDefinitions;
internal class CourseEndpoint : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        const string ENDPOINT = "course";

        app.MediatorPost<CreateCourseCommand, Guid>(ENDPOINT, "/");
    }
}
