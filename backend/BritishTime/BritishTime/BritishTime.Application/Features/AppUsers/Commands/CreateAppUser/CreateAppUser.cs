using FluentValidation;

namespace BritishTime.Application.Features.AppUsers.Commands.CreateAppUser;
public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
{
    public CreateAppUserCommandValidator()
    {
        RuleFor(p => p.AppUser.EmployeeId).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.AppUser.Roles).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.AppUser.Password).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
