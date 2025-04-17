
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Features.BrancheFeatures.Queries.GetUserBranches;

public sealed record GetUserBranchesQueryResponse(List<SelectListDto> result);
