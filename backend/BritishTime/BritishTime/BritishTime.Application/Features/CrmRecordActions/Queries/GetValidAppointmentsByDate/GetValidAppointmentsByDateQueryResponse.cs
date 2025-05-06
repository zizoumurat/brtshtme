
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidAppointmentsByDate;
public sealed record GetValidAppointmentsByDateQueryResponse(IList<AppointmentListDto> result);
