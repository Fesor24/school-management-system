using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Shared;

namespace SMS.Application.Courses.Commands.CreateCourse;

public record CreateCourseCommand(string Name, string Code, int Unit, Guid DepartmentId) :
    IRequest<Result<CreateCourseResponse, Error>>;

internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, 
    Result<CreateCourseResponse, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public CreateCourseCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateCourseResponse, Error>> Handle(CreateCourseCommand request, 
        CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetAsync(request.DepartmentId, cancellationToken);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.DepartmentId);

        var result = department.AddCourse(request.Name, request.Code, request.Unit, department.Id);

        if (result.IsFailure) return result.Error;

        await _departmentRepository.AddCourseAsync(result.Value, cancellationToken);

        await _departmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return _mapper.Map<CreateCourseResponse>(result.Value);
    }
}
