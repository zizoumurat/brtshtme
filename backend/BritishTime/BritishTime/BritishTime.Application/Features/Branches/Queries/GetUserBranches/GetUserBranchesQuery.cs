using MediatR;

namespace BritishTime.Application.Features.BrancheFeatures.Queries.GetUserBranches;

public sealed record GetUserBranchesQuery() : IRequest<GetUserBranchesQueryResponse>;
