using MediatR;


namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidCallsByDate;

public sealed record GetValidCallsByDateQuery(DateTime Date) : IRequest<GetValidCallsByDateQueryResponse>;
