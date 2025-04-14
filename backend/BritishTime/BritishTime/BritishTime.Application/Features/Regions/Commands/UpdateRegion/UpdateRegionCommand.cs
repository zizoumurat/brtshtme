using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Regions.Commands.UpdateRegion;

public sealed record UpdateRegionCommand(RegionDto Region) : IRequest<UpdateRegionCommandResponse>;