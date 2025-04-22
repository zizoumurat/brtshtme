using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Discounts.Commands.CreateDiscount;

public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, CreateDiscountCommandResponse>
{
    private readonly IDiscountService _DiscountService;

    public CreateDiscountCommandHandler(IDiscountService DiscountService)
    {
        _DiscountService = DiscountService;
    }

    public async Task<CreateDiscountCommandResponse> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        await _DiscountService.AddAsync(request.Discount);
        return new("added");
    }
}
