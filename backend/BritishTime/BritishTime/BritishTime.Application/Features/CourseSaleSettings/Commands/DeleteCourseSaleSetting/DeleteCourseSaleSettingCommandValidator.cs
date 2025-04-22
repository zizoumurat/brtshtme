using FluentValidation;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.DeleteCourseSaleSetting;
public class DeleteCourseSaleSettingCommandValidator : AbstractValidator<DeleteCourseSaleSettingCommand>
{
    public DeleteCourseSaleSettingCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
