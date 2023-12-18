using AutoMapper;
using MediatR;
using SMS.Application.Department.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Shared;

namespace SMS.Application.Department.Queries.GetDepartments;
public record GetDepartmentsRequest() : IRequest<Result<IReadOnlyList<GetDepartmentResponse>, Error>>;

internal sealed class GetDepartmentsRequestHandler : IRequestHandler<GetDepartmentsRequest,
    Result<IReadOnlyList<GetDepartmentResponse>, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetDepartmentsRequestHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<GetDepartmentResponse>, Error>> Handle(GetDepartmentsRequest request, 
        CancellationToken cancellationToken)
    {
        var departments = await _departmentRepository.GetAll(cancellationToken);

        return Result.Success<IReadOnlyList<GetDepartmentResponse>, Error>(
            _mapper.Map<IReadOnlyList<GetDepartmentResponse>>(departments));
    }
}
