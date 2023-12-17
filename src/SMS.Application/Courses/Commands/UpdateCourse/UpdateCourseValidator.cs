using FluentValidation;

namespace SMS.Application.Courses.Commands.UpdateCourse;
public sealed class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code can not be empty")
            .NotNull().WithMessage("Code can not be null");

        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name can not be empty")
           .NotNull().WithMessage("Name can not be null");

        RuleFor(x => x.Unit)
           .LessThanOrEqualTo(6).WithMessage("Unit can not be more than 6")
           .GreaterThanOrEqualTo(1).WithMessage("Unit must be greater than 0");
    }
}
