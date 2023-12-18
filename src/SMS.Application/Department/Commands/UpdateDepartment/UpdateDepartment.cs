using MediatR;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Shared;

namespace SMS.Application.Department.Commands.UpdateDepartment;
public record UpdateDepartmentCommand(
    Guid Id,
    string Name,
    string Code
    ) : IRequest<Result<Unit, Error>>;

internal sealed class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<Unit, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<Unit, Error>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetAsync(request.Id);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.Id);

        var result = department.UpdateDepartment(request.Name, request.Code);

        if (result.IsFailure) return DomainErrors.Department.DepartmentBadRequest();

        _departmentRepository.Update(department);

        await _departmentRepository.UnitOfWork.SaveEntitiesAsync();

        return Unit.Value;
    }
}
