using MediatR;
using SMS.Domain.Errors;
using SMS.Domain.Primitives;
using SMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Application.Department.Commands.DeleteDepartment;
public record DeleteDepartmentCommand(Guid Id) : IRequest<Result<Unit, Error>>;

internal sealed class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDepartmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit, Error>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = await _unitOfWork.DepartmentRepository.GetAsync(request.Id);

        if (department is null) return DomainErrors.Department.DepartmentNotFound(request.Id);

        _unitOfWork.DepartmentRepository.Delete(department);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
