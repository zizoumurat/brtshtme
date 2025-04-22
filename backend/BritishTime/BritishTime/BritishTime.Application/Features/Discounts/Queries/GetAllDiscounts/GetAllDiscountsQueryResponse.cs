
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.DiscountsFeatures.Queries.GetAllDiscounts;
public sealed record GetAllDiscountsQueryResponse(PaginatedList<DiscountDto> result);
