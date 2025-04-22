using FluentValidation;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.CreateCourseSaleSetting;
public class CreateCourseSaleSettingCommandValidator : AbstractValidator<CreateCourseSaleSettingCommand>
{
    public CreateCourseSaleSettingCommandValidator()
    {
        RuleFor(p => p.CourseSaleSetting.MinLevel).NotNull().WithMessage("RequiredField");
        RuleFor(p => p.CourseSaleSetting.MaxLevel).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CourseSaleSetting.Rate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CourseSaleSetting.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
