using FluentValidation;

namespace BritishTime.Application.Features.Employees.Commands.UpdateEmployee;
public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(p => p.Employee.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Employee.FirstName).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Employee.LastName).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Employee.IdentityNumber).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Employee.Email).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Employee.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
