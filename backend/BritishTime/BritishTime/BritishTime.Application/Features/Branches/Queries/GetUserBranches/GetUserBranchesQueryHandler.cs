
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.BrancheFeatures.Queries.GetUserBranches;

public sealed class GetUserBranchesQueryHandler : IRequestHandler<GetUserBranchesQuery, GetUserBranchesQueryResponse>
{
    private readonly IBranchService _branchService;

    public GetUserBranchesQueryHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }
    public async Task<GetUserBranchesQueryResponse> Handle(GetUserBranchesQuery request, CancellationToken cancellationToken)
    {
        var result = await _branchService.GetUserBranchListAsync();

        return new(result);
    }
}