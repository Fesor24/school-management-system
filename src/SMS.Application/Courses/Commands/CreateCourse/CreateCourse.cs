﻿using AutoMapper;
using MediatR;
using SMS.Application.Courses.Response;
using SMS.Domain.Exceptions.Department;
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
        var department = await _unitOfWork.DepartmentRepository.GetDepartmentInfo(request.DepartmentId, cancellationToken) ??
            throw new DepartmentNotFoundException(request.DepartmentId);

        var result = department.AddCourse(request.Name, request.Code, request.Unit);

        //if (result.IsFailure && result.Error.Equals(DomainErrors.Course.InvalidCourseUnit))
        //    throw new CourseInvalidUnitException(result.Error.Message);

        if (result.IsFailure) return Result.Failure<CreateCourseResponse, Error>(result.Error);

        //Course course = result.Value;

        _unitOfWork.DepartmentRepository.Update(department);

        return Result.Success<CreateCourseResponse, Error>(_mapper.Map<CreateCourseResponse>(result));
    }
}
