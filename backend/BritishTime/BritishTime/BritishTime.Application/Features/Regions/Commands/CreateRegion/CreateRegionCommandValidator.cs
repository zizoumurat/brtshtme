using FluentValidation;

namespace BritishTime.Application.Features.Regions.Commands.CreateRegion;
public class CreateRegionCommandValidator : AbstractValidator<CreateRegionCommand>
{
    public CreateRegionCommandValidator()
    {
        RuleFor(p => p.Region.Name).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
