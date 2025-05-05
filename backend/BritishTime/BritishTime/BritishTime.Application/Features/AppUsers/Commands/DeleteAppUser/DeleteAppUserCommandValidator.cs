using FluentValidation;

namespace BritishTime.Application.Features.AppUsers.Commands.DeleteAppUser;
public class DeleteAppUserCommandValidator : AbstractValidator<DeleteAppUserCommand>
{
    public DeleteAppUserCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
