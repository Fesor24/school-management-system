using MediatR;
using SMS.Domain.Exceptions.Course;
using SMS.Domain.Exceptions.Department;
using SMS.Domain.Primitives;

namespace SMS.Application.Courses.Commands.DeleteCourse;
internal record DeleteCourseCommand(Guid CourseId, Guid DepartmentId) : IRequest<Unit>;

internal sealed class DeleteCourseCommadHandler : IRequestHandler<DeleteCourseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCourseCommadHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetDepartmentInfo(request.DepartmentId) ??
            throw new DepartmentNotFoundException(request.DepartmentId);

        var result = department.RemoveCourse(request.CourseId);

        if (result.IsFailure)
            throw new CourseNotFoundException(request.CourseId);

        _unitOfWork.DepartmentRepository.Update(department);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
