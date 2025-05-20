using FluentValidation;

namespace BritishTime.Application.Features.CourseClasses.Commands.CreateCourseClass;
public class CreateCourseClassCommandValidator : AbstractValidator<CreateCourseClassCommand>
{
    public CreateCourseClassCommandValidator()
    {
        RuleFor(p => p.CourseClass.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
