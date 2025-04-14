using FluentValidation;

namespace BritishTime.Application.Features.IncentiveSettinges.Commands.CreateIncentiveSetting;
public class CreateIncentiveSettingCommandValidator : AbstractValidator<CreateIncentiveSettingCommand>
{
    public CreateIncentiveSettingCommandValidator()
    {
        RuleFor(p => p.IncentiveSetting.MinAmount).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.IncentiveSetting.MaxAmount).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.IncentiveSetting.SalesCommission).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.IncentiveSetting.CollectionCommission).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
