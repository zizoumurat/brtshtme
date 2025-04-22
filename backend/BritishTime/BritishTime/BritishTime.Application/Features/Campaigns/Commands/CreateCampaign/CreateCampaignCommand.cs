using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Campaigns.Commands.CreateCampaign;

public sealed record CreateCampaignCommand(CampaignCreateDto Campaign) : IRequest<CreateCampaignCommandResponse>;