using FluentValidation;

namespace BritishTime.Application.Features.Branches.Commands.UpdateBranch;
public class UpdateBranchCommandValidator : AbstractValidator<UpdateBranchCommand>
{
    public UpdateBranchCommandValidator()
    {
        RuleFor(p => p.Branch.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Branch.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
