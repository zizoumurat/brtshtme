using FluentValidation;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.DeleteInstallmentSetting;
public class DeleteInstallmentSettingCommandValidator : AbstractValidator<DeleteInstallmentSettingCommand>
{
    public DeleteInstallmentSettingCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
