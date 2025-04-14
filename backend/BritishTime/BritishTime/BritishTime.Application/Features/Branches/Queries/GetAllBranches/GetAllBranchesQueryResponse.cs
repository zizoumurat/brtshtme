
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.BrancheFeatures.Queries.GetAllBranches;

public sealed record GetAllBranchesQueryResponse(PaginatedList<BranchDto> result);
