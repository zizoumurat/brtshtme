using FluentValidation;

namespace BritishTime.Application.Features.Campaigns.Commands.UpdateCampaign;
public class UpdateCampaignCommandValidator : AbstractValidator<UpdateCampaignCommand>
{
    public UpdateCampaignCommandValidator()
    {
        RuleFor(p => p.Campaign.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Campaign.Definition).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Campaign.DiscountRate).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Campaign.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
