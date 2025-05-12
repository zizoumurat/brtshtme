using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Levels.Commands.CreateLevel;

public class CreateLevelCommandHandler : IRequestHandler<CreateLevelCommand, CreateLevelCommandResponse>
{
    private readonly ILevelService _LevelService;

    public CreateLevelCommandHandler(ILevelService LevelService)
    {
        _LevelService = LevelService;
    }

    public async Task<CreateLevelCommandResponse> Handle(CreateLevelCommand request, CancellationToken cancellationToken)
    {
        await _LevelService.AddAsync(request.Level);
        return new("added");
    }
}
