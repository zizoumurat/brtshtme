using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Campaigns.Commands.DeleteCampaign;

public sealed record DeleteCampaignCommand(Guid Id) : IRequest<DeleteCampaignCommandResponse>;