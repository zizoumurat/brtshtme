
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetOpenCalls;
public sealed record GetOpenCallsQueryResponse(IList<AppointmentListDto> result);
