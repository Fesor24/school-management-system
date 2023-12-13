using MediatR;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Application.Department.Commands.UpdateDepartment;
public record UpdateDepartmentCommand(
    Guid Id,
    string Name,
    string Code
    ) : IRequest<Result<Unit, Error>>;

internal sealed class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit, Error>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetAsync(request.Id);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.Id);

        var result = department.UpdateDepartment(request.Name, request.Code);

        if (result.IsFailure) return DomainErrors.Department.DepartmentBadRequest();

        _unitOfWork.DepartmentRepository.Update(department);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
