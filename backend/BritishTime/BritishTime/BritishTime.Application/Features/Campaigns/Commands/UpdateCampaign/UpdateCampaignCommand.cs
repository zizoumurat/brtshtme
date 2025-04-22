using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Campaigns.Commands.UpdateCampaign;

public sealed record UpdateCampaignCommand(CampaignDto Campaign) : IRequest<UpdateCampaignCommandResponse>;