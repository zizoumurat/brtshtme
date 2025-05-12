
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.LevelsFeatures.Queries.GetAllLevels;

public sealed class GetAllLevelsQueryHandler : IRequestHandler<GetAllLevelsQuery, GetAllLevelsQueryResponse>
{
    private readonly ILevelService _LevelService;

    public GetAllLevelsQueryHandler(ILevelService LevelService)
    {
        _LevelService = LevelService;
    }
    public async Task<GetAllLevelsQueryResponse> Handle(GetAllLevelsQuery request, CancellationToken cancellationToken)
    {
        var result = await _LevelService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}