using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;


namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidAppointmentsByDate;

public sealed record GetValidAppointmentsByDateQuery(DateTime Date) : IRequest<GetValidAppointmentsByDateQueryResponse>;
