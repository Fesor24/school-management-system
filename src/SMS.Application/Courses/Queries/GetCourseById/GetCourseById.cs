using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Application.Courses.Queries.GetCourseById;
public record GetCourseByIdRequest(Guid Id) : IRequest<Result<GetCourseResponse, Error>>;

internal sealed class GetCourseByIdRequestHandler : IRequestHandler<GetCourseByIdRequest, 
    Result<GetCourseResponse, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCourseByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetCourseResponse, Error>> Handle(GetCourseByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.DepartmentRepository.GetCourseAsync(request.Id, cancellationToken);

        if (course is null) return DomainErrors.Course.CourseNotFound(request.Id);

        return _mapper.Map<GetCourseResponse>(course);
    }
}
