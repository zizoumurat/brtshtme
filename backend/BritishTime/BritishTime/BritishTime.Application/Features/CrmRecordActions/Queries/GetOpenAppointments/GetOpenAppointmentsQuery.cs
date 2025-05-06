using MediatR;


namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetOpenAppointments;

public sealed record GetOpenAppointmentsQuery() : IRequest<GetOpenAppointmentsQueryResponse>;
