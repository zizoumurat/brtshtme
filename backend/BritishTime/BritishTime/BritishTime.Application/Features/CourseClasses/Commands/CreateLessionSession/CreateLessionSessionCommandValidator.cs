using FluentValidation;

namespace BritishTime.Application.Features.CourseClasses.Commands.CreateLessionSession;
public class CreateLessionSessionCommandValidator : AbstractValidator<CreateLessionSessionCommand>
{
    public CreateLessionSessionCommandValidator()
    {
        RuleFor(p => p.request.classId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
