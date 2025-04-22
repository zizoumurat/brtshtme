
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetAllCampaigns;
public sealed record GetAllCampaignsQueryResponse(PaginatedList<CampaignDto> result);
