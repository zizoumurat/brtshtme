using FluentValidation;

namespace BritishTime.Application.Features.CrmRecords.Commands.UpdateCrmRecord;
public class UpdateCrmRecordCommandValidator : AbstractValidator<UpdateCrmRecordCommand>
{
    public UpdateCrmRecordCommandValidator()
    {
        RuleFor(p => p.CrmRecord.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.FirstName).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.LastName).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.Phone).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
