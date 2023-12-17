using FluentValidation;

namespace SMS.Application.Department.Commands.UpdateDepartment;
public sealed class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code can not be empty")
            .NotNull().WithMessage("Code can not be null");

        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name can not be empty")
           .NotNull().WithMessage("Name can not be null");
    }
}
