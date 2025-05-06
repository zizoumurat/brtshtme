
using BritishTime.Domain.Dtos;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetOpenAppointments;
public sealed record GetOpenAppointmentsQueryResponse(IList<AppointmentListDto> result);
