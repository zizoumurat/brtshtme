using FluentValidation;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.UpdateCourseSaleSetting;
public class UpdateCourseSaleSettingCommandValidator : AbstractValidator<UpdateCourseSaleSettingCommand>
{
    public UpdateCourseSaleSettingCommandValidator()
    {
        RuleFor(p => p.CourseSaleSetting.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CourseSaleSetting.MinLevel).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CourseSaleSetting.MaxLevel).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CourseSaleSetting.Rate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CourseSaleSetting.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
