using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Regiones.Commands.CreateRegion;

public sealed record CreateRegionCommand(RegionCreateDto Region) : IRequest<CreateRegionCommandResponse>;