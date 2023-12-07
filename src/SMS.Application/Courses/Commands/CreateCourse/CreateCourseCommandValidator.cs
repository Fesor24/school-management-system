using FluentValidation;

namespace SMS.Application.Courses.Commands.CreateCourse;
internal class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Course code can not be empty")
            .NotNull().WithMessage("Course code can not be null");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course name can not be empty")
            .NotNull().WithMessage("Course name can not be null");
    }
}
