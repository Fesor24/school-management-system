using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Exceptions.Department;
using SMS.Domain.Primitives;

namespace SMS.Application.Courses.Queries.GetDepartmentCourses;
internal record GetDepartmentCoursesRequest(Guid DepartmentId) : IRequest<List<GetCourseResponse>>;

internal sealed class GetCoursesRequestHandler : IRequestHandler<GetDepartmentCoursesRequest, List<GetCourseResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCoursesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetCourseResponse>> Handle(GetDepartmentCoursesRequest request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetDepartmentInfo(request.DepartmentId) ??
            throw new DepartmentNotFoundException(request.DepartmentId);

        return _mapper.Map<List<GetCourseResponse>>(department.Courses);
    }
}
