using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Exceptions.Department;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Application.Courses.Queries.GetDepartmentCourses;
public record GetDepartmentCoursesRequest(Guid DepartmentId) : IRequest<Result<List<GetCourseResponse>, Error>>;

internal sealed class GetDepartmentCoursesRequestHandler : IRequestHandler<GetDepartmentCoursesRequest,
    Result<List<GetCourseResponse>, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetDepartmentCoursesRequestHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<GetCourseResponse>, Error>> Handle(GetDepartmentCoursesRequest request, 
        CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentInfo(request.DepartmentId) ??
            throw new DepartmentNotFoundException(request.DepartmentId);

        return _mapper.Map<List<GetCourseResponse>>(department.Courses);
    }
}
