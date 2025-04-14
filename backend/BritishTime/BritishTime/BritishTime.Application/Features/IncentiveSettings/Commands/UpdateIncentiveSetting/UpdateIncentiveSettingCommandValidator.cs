using FluentValidation;

namespace BritishTime.Application.Features.IncentiveSettings.Commands.UpdateIncentiveSetting;
public class UpdateIncentiveSettingCommandValidator : AbstractValidator<UpdateIncentiveSettingCommand>
{
    public UpdateIncentiveSettingCommandValidator()
    {
        RuleFor(p => p.IncentiveSetting.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.IncentiveSetting.MinAmount).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.IncentiveSetting.MaxAmount).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.IncentiveSetting.SalesCommission).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.IncentiveSetting.CollectionCommission).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
