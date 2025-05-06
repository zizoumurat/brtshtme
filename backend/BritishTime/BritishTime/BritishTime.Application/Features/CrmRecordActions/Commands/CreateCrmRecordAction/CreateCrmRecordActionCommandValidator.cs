using FluentValidation;

namespace BritishTime.Application.Features.CrmRecordActions.Commands.CreateCrmRecordAction;
public class CreateCrmRecordActionCommandValidator : AbstractValidator<CreateCrmRecordActionCommand>
{
    public CreateCrmRecordActionCommandValidator()
    {
        RuleFor(p => p.CrmRecordAction.ActionType).NotNull().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecordAction.CrmRecordId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
