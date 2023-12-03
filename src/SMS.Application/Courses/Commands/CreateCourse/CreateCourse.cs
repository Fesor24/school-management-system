using MediatR;
using SMS.Domain.Entities;

namespace SMS.Application.Courses.Commands.CreateCourse;
internal record CreateCourseCommand(string Name, string Code, int Unit) : IRequest<int>;

internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    public Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Course.Create(
            Guid.NewGuid(),
            request.Name,
            request.Code,
            request.Unit);

        throw new NotImplementedException();
    }
}
