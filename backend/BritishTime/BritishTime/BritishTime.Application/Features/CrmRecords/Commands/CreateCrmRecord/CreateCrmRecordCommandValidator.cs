using FluentValidation;

namespace BritishTime.Application.Features.CrmRecords.Commands.CreateCrmRecord;
public class CreateCrmRecordCommandValidator : AbstractValidator<CreateCrmRecordCommand>
{
    public CreateCrmRecordCommandValidator()
    {
        RuleFor(p => p.CrmRecord.FirstName).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.LastName).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.Phone).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.Email).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CrmRecord.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
