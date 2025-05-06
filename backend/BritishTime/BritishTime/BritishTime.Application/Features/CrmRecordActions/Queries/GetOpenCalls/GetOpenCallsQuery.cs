using MediatR;


namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetOpenCalls;

public sealed record GetOpenCallsQuery() : IRequest<GetOpenCallsQueryResponse>;
