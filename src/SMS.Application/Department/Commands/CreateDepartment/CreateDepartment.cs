using MediatR;
using SMS.Application.Department.Response;
using DepartmentEntity = SMS.Domain.Aggregates.DepartmentAggregates.Department;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;
using AutoMapper;

namespace SMS.Application.Department.Commands.CreateDepartment;
public sealed record CreateDepartmentCommand(string Name, string Code) : IRequest<Result<CreateDepartmentResponse, Error>>;

internal sealed class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, 
    Result<CreateDepartmentResponse, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateDepartmentResponse, Error>> Handle(CreateDepartmentCommand request, 
        CancellationToken cancellationToken)
    {
        Result<DepartmentEntity, Error> result = DepartmentEntity.Create(request.Name, request.Code);

        if (result.IsFailure) return Result.Failure<CreateDepartmentResponse, Error>(result.Error);

        await _unitOfWork.DepartmentRepository.AddAsync(result.Value);

        await _unitOfWork.Complete();

        return Result.Success<CreateDepartmentResponse, Error>(
            _mapper.Map<CreateDepartmentResponse>(result.Value));
    }
}
