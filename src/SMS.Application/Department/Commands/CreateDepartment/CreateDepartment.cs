using AutoMapper;
using MediatR;
using SMS.Application.Courses.Request;
using SMS.Application.Department.Response;
using SMS.Domain.Models.Course;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;
using DepartmentEntity = SMS.Domain.Aggregates.DepartmentAggregates.Department;

namespace SMS.Application.Department.Commands.CreateDepartment;
public sealed record CreateDepartmentCommand(string Name, string Code, List<CreateCourseRequest> Courses) : 
    IRequest<Result<CreateDepartmentResponse, Error>>;

internal sealed class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, 
    Result<CreateDepartmentResponse, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateDepartmentResponse, Error>> Handle(CreateDepartmentCommand request, 
        CancellationToken cancellationToken)
    {
        Result<DepartmentEntity, Error> result = DepartmentEntity.Create(request.Name, request.Code, 
            _mapper.Map<List<CourseModel>>(request.Courses));

        if (result.IsFailure) return result.Error;

        await _unitOfWork.DepartmentRepository.AddAsync(result.Value);

        await _unitOfWork.Complete();

        return _mapper.Map<CreateDepartmentResponse>(result.Value);
    }
}
