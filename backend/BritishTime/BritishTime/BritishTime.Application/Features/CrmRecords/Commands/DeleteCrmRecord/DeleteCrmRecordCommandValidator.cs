using FluentValidation;

namespace BritishTime.Application.Features.CrmRecords.Commands.DeleteCrmRecord;
public class DeleteCrmRecordCommandValidator : AbstractValidator<DeleteCrmRecordCommand>
{
    public DeleteCrmRecordCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
