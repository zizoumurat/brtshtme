using FluentValidation;

namespace BritishTime.Application.Features.Branches.Commands.CreateBranch;
public class CreateBranchCommandValidator : AbstractValidator<CreateBranchCommand>
{
    public CreateBranchCommandValidator()
    {
        RuleFor(p => p.Branch.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
