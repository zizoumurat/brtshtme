using FluentValidation;

namespace BritishTime.Application.Features.Discounts.Commands.UpdateDiscount;
public class UpdateDiscountCommandValidator : AbstractValidator<UpdateDiscountCommand>
{
    public UpdateDiscountCommandValidator()
    {
        RuleFor(p => p.Discount.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Discount.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Discount.DiscountRate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Discount.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
