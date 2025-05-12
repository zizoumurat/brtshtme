using FluentValidation;

namespace BritishTime.Application.Features.Levels.Commands.DeleteLevel;
public class DeleteLevelCommandValidator : AbstractValidator<DeleteLevelCommand>
{
    public DeleteLevelCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
