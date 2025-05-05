
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.AppUsersFeatures.Queries.GetAllAppUsers;

public sealed class GetAllAppUsersQueryHandler : IRequestHandler<GetAllAppUsersQuery, GetAllAppUsersQueryResponse>
{
    private readonly IAppUserService _AppUserService;

    public GetAllAppUsersQueryHandler(IAppUserService AppUserService)
    {
        _AppUserService = AppUserService;
    }
    public async Task<GetAllAppUsersQueryResponse> Handle(GetAllAppUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _AppUserService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}