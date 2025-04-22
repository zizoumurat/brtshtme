using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Discounts.Commands.CreateDiscount;

public sealed record CreateDiscountCommand(DiscountCreateDto Discount) : IRequest<CreateDiscountCommandResponse>;