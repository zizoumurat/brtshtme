using FluentValidation;

namespace BritishTime.Application.Features.Campaigns.Commands.DeleteCampaign;
public class DeleteCampaignCommandValidator : AbstractValidator<DeleteCampaignCommand>
{
    public DeleteCampaignCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
