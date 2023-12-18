using MediatR;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Errors;
using SMS.Domain.Shared;

namespace SMS.Application.Department.Commands.DeleteDepartment;
public record DeleteDepartmentCommand(Guid Id) : IRequest<Result<Unit, Error>>;

internal sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result<Unit, Error>>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Result<Unit, Error>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetAsync(request.Id);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.Id);

        _departmentRepository.Delete(department);

        await _departmentRepository.UnitOfWork.SaveEntitiesAsync();

        return Unit.Value;
    }
}
