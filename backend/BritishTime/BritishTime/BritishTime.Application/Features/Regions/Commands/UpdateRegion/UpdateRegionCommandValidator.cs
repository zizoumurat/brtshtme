using FluentValidation;

namespace BritishTime.Application.Features.Regions.Commands.UpdateRegion;
public class UpdateRegionCommandValidator : AbstractValidator<UpdateRegionCommand>
{
    public UpdateRegionCommandValidator()
    {
        RuleFor(p => p.Region.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.Region.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
