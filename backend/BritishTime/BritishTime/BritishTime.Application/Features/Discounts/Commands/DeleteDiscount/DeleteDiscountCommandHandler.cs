using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Discounts.Commands.DeleteDiscount;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, DeleteDiscountCommandResponse>
{
    private readonly IDiscountService _DiscountService;

    public DeleteDiscountCommandHandler(IDiscountService DiscountService)
    {
        _DiscountService = DiscountService;
    }

    public async Task<DeleteDiscountCommandResponse> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        await _DiscountService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
