
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.AppUsersFeatures.Queries.GetUnassignedEmployees;

public sealed class GetUnassignedEmployeesQueryHandler : IRequestHandler<GetUnassignedEmployeesQuery, GetUnassignedEmployeesQueryResponse>
{
    private readonly IAppUserService _appUserService;

    public GetUnassignedEmployeesQueryHandler(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }
    public async Task<GetUnassignedEmployeesQueryResponse> Handle(GetUnassignedEmployeesQuery request, CancellationToken cancellationToken)
    {
        var result = await _appUserService.GetUnassignedEmployees(request.BranchId);

        return new(result);
    }
}