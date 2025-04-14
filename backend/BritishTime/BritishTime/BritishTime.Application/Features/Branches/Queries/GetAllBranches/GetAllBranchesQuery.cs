using BritishTime.Application.Features.Auth.Login;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using MediatR;
using TS.Result;


namespace BritishTime.Application.Features.BrancheFeatures.Queries.GetAllBranches;

public sealed record GetAllBranchesQuery
    (BranchFilterDto filter, PageRequest pagination) : IRequest<GetAllBranchesQueryResponse>;
