using FluentValidation;

namespace BritishTime.Application.Features.Regions.Commands.DeleteRegion;
public class DeleteRegionCommandValidator : AbstractValidator<DeleteRegionCommand>
{
    public DeleteRegionCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
