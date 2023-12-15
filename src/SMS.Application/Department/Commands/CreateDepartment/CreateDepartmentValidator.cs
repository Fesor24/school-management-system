using FluentValidation;

namespace SMS.Application.Department.Commands.CreateDepartment;
public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code can not be empty")
            .NotNull().WithMessage("Code can not be null");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name can not be empty")
            .NotNull().WithMessage("Name can not be null");
    }
}
