using FluentValidation;

namespace SMS.Application.Courses.Commands.CreateCourse;
internal sealed class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Course code can not be empty")
            .NotNull().WithMessage("Course code can not be null");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course name can not be empty")
            .NotNull().WithMessage("Course name can not be null");

        RuleFor(x => x.Unit)
            .LessThanOrEqualTo(6).WithMessage("Unit can not be more than 6")
            .GreaterThanOrEqualTo(1).WithMessage("Unit must be greater than 0");
    }
}
