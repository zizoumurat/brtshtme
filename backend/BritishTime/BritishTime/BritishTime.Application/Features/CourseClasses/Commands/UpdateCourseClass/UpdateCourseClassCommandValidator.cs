using FluentValidation;

namespace BritishTime.Application.Features.CourseClasses.Commands.UpdateCourseClass;
public class UpdateCourseClassCommandValidator : AbstractValidator<UpdateCourseClassCommand>
{
    public UpdateCourseClassCommandValidator()
    {
        RuleFor(p => p.CourseClass.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CourseClass.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
