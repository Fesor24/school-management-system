using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Application.Courses.Commands.CreateCourse;
public record CreateCourseCommand(string Name, string Code, int Unit, Guid DepartmentId) : 
    IRequest<Result<CreateCourseResponse, Error>>;

internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, 
    Result<CreateCourseResponse, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCourseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateCourseResponse, Error>> Handle(CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetDepartmentInfo(request.DepartmentId, cancellationToken);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.DepartmentId);

        var result = department.AddCourse(request.Name, request.Code, request.Unit);

        if (result.IsFailure) return Result.Failure<CreateCourseResponse, Error>(result.Error);

        _unitOfWork.DepartmentRepository.Update(department);

        return Result.Success<CreateCourseResponse, Error>(_mapper.Map<CreateCourseResponse>(result.Value));
    }
}
