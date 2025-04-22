using FluentValidation;

namespace BritishTime.Application.Features.Discounts.Commands.CreateDiscount;
public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    public CreateDiscountCommandValidator()
    {
        RuleFor(p => p.Discount.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Discount.DiscountRate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Discount.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
