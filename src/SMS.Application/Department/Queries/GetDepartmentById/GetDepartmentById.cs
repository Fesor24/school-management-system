using AutoMapper;
using MediatR;
using SMS.Application.Department.Response;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Application.Department.Queries.GetDepartmentById;
public record GetDepartmentByIdRequest(Guid Id) : IRequest<Result<GetDepartmentResponse, Error>>;

internal sealed class GetDepartmentByIdRequestHandler : IRequestHandler<GetDepartmentByIdRequest,
    Result<GetDepartmentResponse, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDepartmentByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetDepartmentResponse, Error>> Handle(GetDepartmentByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetDepartmentInfo(request.Id);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.Id);

        return _mapper.Map<GetDepartmentResponse>(department);
    }
}
