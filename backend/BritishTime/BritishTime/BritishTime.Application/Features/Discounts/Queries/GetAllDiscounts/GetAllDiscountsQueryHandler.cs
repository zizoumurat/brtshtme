
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.DiscountsFeatures.Queries.GetAllDiscounts;

public sealed class GetAllDiscountsQueryHandler : IRequestHandler<GetAllDiscountsQuery, GetAllDiscountsQueryResponse>
{
    private readonly IDiscountService _DiscountService;

    public GetAllDiscountsQueryHandler(IDiscountService DiscountService)
    {
        _DiscountService = DiscountService;
    }
    public async Task<GetAllDiscountsQueryResponse> Handle(GetAllDiscountsQuery request, CancellationToken cancellationToken)
    {
        var result = await _DiscountService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}