using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Discounts.Commands.UpdateDiscount;

public sealed record UpdateDiscountCommand(DiscountDto Discount) : IRequest<UpdateDiscountCommandResponse>;