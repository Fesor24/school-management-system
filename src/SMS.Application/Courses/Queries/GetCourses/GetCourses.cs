using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Abstractions;

namespace SMS.Application.Courses.Queries.GetCourses;
internal record GetCoursesRequest : IRequest<List<GetCourseResponse>>;

internal sealed class GetCoursesRequestHandler : IRequestHandler<GetCoursesRequest, List<GetCourseResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCoursesRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetCourseResponse>> Handle(GetCoursesRequest request, CancellationToken cancellationToken)
    {
        var courses = await _unitOfWork.CourseRepository.GetAll(cancellationToken);

        return _mapper.Map<List<GetCourseResponse>>(courses);
    }
}
