using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Levels.Commands.UpdateLevel;

public class UpdateLevelCommandHandler : IRequestHandler<UpdateLevelCommand, UpdateLevelCommandResponse>
{
    private readonly ILevelService _LevelService;

    public UpdateLevelCommandHandler(ILevelService LevelService)
    {
        _LevelService = LevelService;
    }

    public async Task<UpdateLevelCommandResponse> Handle(UpdateLevelCommand request, CancellationToken cancellationToken)
    {
        await _LevelService.UpdateAsync(request.Level);
        return new("updated");
    }
}
