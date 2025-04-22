using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetAllCampaigns;

public sealed record GetAllCampaignsQuery
    (CampaignFilterDto filter, PageRequest pagination) : IRequest<GetAllCampaignsQueryResponse>;
