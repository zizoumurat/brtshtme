
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidCallsByDate;
public sealed record GetValidCallsByDateQueryResponse(IList<AppointmentListDto> result);
