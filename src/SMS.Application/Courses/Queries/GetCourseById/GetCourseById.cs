using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Shared;

namespace SMS.Application.Courses.Queries.GetCourseById;
public record GetCourseByIdRequest(Guid Id) : IRequest<Result<GetCourseResponse, Error>>;

internal sealed class GetCourseByIdRequestHandler : IRequestHandler<GetCourseByIdRequest, 
    Result<GetCourseResponse, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetCourseByIdRequestHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetCourseResponse, Error>> Handle(GetCourseByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var course = await _departmentRepository.GetCourseAsync(request.Id, cancellationToken);

        if (course is null) return DomainErrors.Course.CourseNotFound(request.Id);

        return _mapper.Map<GetCourseResponse>(course);
    }
}
