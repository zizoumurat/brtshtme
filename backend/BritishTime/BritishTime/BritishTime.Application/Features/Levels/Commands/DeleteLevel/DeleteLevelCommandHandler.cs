using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Levels.Commands.DeleteLevel;

public class DeleteLevelCommandHandler : IRequestHandler<DeleteLevelCommand, DeleteLevelCommandResponse>
{
    private readonly ILevelService _LevelService;

    public DeleteLevelCommandHandler(ILevelService LevelService)
    {
        _LevelService = LevelService;
    }

    public async Task<DeleteLevelCommandResponse> Handle(DeleteLevelCommand request, CancellationToken cancellationToken)
    {
        await _LevelService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
