using FluentValidation;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.CreateInstallmentSetting;
public class CreateInstallmentSettingCommandValidator : AbstractValidator<CreateInstallmentSettingCommand>
{
    public CreateInstallmentSettingCommandValidator()
    {
        RuleFor(p => p.InstallmentSetting.Level).NotNull().WithMessage("RequiredField");
        RuleFor(p => p.InstallmentSetting.MaxBond).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.InstallmentSetting.MaxCardInstallment).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.InstallmentSetting.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
