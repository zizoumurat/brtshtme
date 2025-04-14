using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Regions.Commands.DeleteRegion;

public sealed record DeleteRegionCommand(Guid Id) : IRequest<DeleteRegionCommandResponse>;