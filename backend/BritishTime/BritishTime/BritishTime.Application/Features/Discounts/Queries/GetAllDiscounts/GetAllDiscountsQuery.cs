using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.DiscountsFeatures.Queries.GetAllDiscounts;

public sealed record GetAllDiscountsQuery
    (DiscountFilterDto filter, PageRequest pagination) : IRequest<GetAllDiscountsQueryResponse>;
