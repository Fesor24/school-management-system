using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Exceptions.Course;
using SMS.Domain.Primitives;

namespace SMS.Application.Courses.Queries.GetCourseById;
internal record GetCourseByIdRequest(Guid CourseId) : IRequest<GetCourseResponse>;

internal sealed class GetCourseByIdRequestHandler : IRequestHandler<GetCourseByIdRequest, GetCourseResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCourseByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetCourseResponse> Handle(GetCourseByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.DepartmentRepository.GetCourseAsync(request.CourseId, cancellationToken) ??
            throw new CourseNotFoundException(request.CourseId);

        return _mapper.Map<GetCourseResponse>(course);
    }
}
