using FluentValidation;

namespace BritishTime.Application.Features.IncentiveSettings.Commands.DeleteIncentiveSetting;
public class DeleteIncentiveSettingCommandValidator : AbstractValidator<DeleteIncentiveSettingCommand>
{
    public DeleteIncentiveSettingCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
