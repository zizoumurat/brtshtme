using FluentValidation;

namespace BritishTime.Application.Features.Students.Commands.CreateStudent;
public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(p => p.StudentRequest).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.CalculatePaymentRequest).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
