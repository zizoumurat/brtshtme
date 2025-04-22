using FluentValidation;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.UpdateInstallmentSetting;
public class UpdateInstallmentSettingCommandValidator : AbstractValidator<UpdateInstallmentSettingCommand>
{
    public UpdateInstallmentSettingCommandValidator()
    {
        RuleFor(p => p.InstallmentSetting.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.InstallmentSetting.Level).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.InstallmentSetting.MaxBond).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.InstallmentSetting.MaxCardInstallment).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.InstallmentSetting.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
