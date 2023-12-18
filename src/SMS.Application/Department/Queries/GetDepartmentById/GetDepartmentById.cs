using AutoMapper;
using MediatR;
using SMS.Application.Department.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Shared;

namespace SMS.Application.Department.Queries.GetDepartmentById;
public record GetDepartmentByIdRequest(Guid Id) : IRequest<Result<GetDepartmentResponse, Error>>;

internal sealed class GetDepartmentByIdRequestHandler : IRequestHandler<GetDepartmentByIdRequest,
    Result<GetDepartmentResponse, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetDepartmentByIdRequestHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetDepartmentResponse, Error>> Handle(GetDepartmentByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetDepartmentInfo(request.Id);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.Id);

        return _mapper.Map<GetDepartmentResponse>(department);
    }
}
