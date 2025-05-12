using FluentValidation;

namespace BritishTime.Application.Features.Levels.Commands.CreateLevel;
public class CreateLevelCommandValidator : AbstractValidator<CreateLevelCommand>
{
    public CreateLevelCommandValidator()
    {
        RuleFor(p => p.Level.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
