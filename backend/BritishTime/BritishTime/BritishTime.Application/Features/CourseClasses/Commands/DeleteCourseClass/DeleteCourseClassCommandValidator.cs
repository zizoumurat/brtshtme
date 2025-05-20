using FluentValidation;

namespace BritishTime.Application.Features.CourseClasses.Commands.DeleteCourseClass;
public class DeleteCourseClassCommandValidator : AbstractValidator<DeleteCourseClassCommand>
{
    public DeleteCourseClassCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
