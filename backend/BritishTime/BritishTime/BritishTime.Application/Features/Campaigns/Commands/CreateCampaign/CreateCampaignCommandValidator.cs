using FluentValidation;

namespace BritishTime.Application.Features.Campaigns.Commands.CreateCampaign;
public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
{
    public CreateCampaignCommandValidator()
    {
        RuleFor(p => p.Campaign.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Campaign.DiscountRate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Campaign.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
