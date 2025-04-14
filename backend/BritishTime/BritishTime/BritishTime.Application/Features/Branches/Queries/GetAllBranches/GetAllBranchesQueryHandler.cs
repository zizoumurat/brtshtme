
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;

namespace BritishTime.Application.Features.BrancheFeatures.Queries.GetAllBranches;

public sealed class GetAllBranchesQueryHandler : IRequestHandler<GetAllBranchesQuery, GetAllBranchesQueryResponse>
{
    private readonly IBranchService _branchService;

    public GetAllBranchesQueryHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }
    public async Task<GetAllBranchesQueryResponse> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
    {
        var result = await _branchService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}