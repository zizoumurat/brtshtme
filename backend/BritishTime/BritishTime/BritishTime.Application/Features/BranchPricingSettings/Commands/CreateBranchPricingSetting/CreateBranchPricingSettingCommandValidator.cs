using FluentValidation;

namespace BritishTime.Application.Features.BranchPricingSettings.Commands.CreateBranchPricingSetting;
public class CreateBranchPricingSettingCommandValidator : AbstractValidator<CreateBranchPricingSettingCommand>
{
    public CreateBranchPricingSettingCommandValidator()
    {
        RuleFor(p => p.BranchPricingSetting.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.BranchPricingSetting.CashPrepaymentDiscount).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.BranchPricingSetting.CollectionRateForBonus).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.BranchPricingSetting.CreditCardInstallmentDiscount).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
