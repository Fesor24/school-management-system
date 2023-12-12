using AutoMapper;
using MediatR;
using SMS.Application.Department.Response;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.Department.Queries.GetDepartments;
public record GetDepartmentsRequest() : IRequest<Result<IReadOnlyList<GetDepartmentResponse>, Error>>;

internal sealed class GetDepartmentsRequestHandler : IRequestHandler<GetDepartmentsRequest,
    Result<IReadOnlyList<GetDepartmentResponse>, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDepartmentsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<GetDepartmentResponse>, Error>> Handle(GetDepartmentsRequest request, 
        CancellationToken cancellationToken)
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAll(cancellationToken);

        return Result.Success<IReadOnlyList<GetDepartmentResponse>, Error>(
            _mapper.Map<IReadOnlyList<GetDepartmentResponse>>(departments));
    }
}
