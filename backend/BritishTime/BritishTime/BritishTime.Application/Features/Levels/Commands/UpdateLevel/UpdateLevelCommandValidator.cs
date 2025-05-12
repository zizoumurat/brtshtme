using FluentValidation;

namespace BritishTime.Application.Features.Levels.Commands.UpdateLevel;
public class UpdateLevelCommandValidator : AbstractValidator<UpdateLevelCommand>
{
    public UpdateLevelCommandValidator()
    {
        RuleFor(p => p.Level.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Level.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
