using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Discounts.Commands.UpdateDiscount;

public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, UpdateDiscountCommandResponse>
{
    private readonly IDiscountService _DiscountService;

    public UpdateDiscountCommandHandler(IDiscountService DiscountService)
    {
        _DiscountService = DiscountService;
    }

    public async Task<UpdateDiscountCommandResponse> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        await _DiscountService.UpdateAsync(request.Discount);
        return new("updated");
    }
}
