using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Discounts.Commands.DeleteDiscount;

public sealed record DeleteDiscountCommand(Guid Id) : IRequest<DeleteDiscountCommandResponse>;