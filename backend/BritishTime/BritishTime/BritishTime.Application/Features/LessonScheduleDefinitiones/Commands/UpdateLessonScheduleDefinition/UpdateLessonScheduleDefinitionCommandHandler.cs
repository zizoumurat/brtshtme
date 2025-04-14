using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.LessonScheduleDefinitions.Commands.UpdateLessonScheduleDefinition;

public class UpdateLessonScheduleDefinitionCommandHandler : IRequestHandler<UpdateLessonScheduleDefinitionCommand, UpdateLessonScheduleDefinitionCommandResponse>
{
    private readonly ILessonScheduleDefinitionService _LessonScheduleDefinitionService;

    public UpdateLessonScheduleDefinitionCommandHandler(ILessonScheduleDefinitionService LessonScheduleDefinitionService)
    {
        _LessonScheduleDefinitionService = LessonScheduleDefinitionService;
    }

    public async Task<UpdateLessonScheduleDefinitionCommandResponse> Handle(UpdateLessonScheduleDefinitionCommand request, CancellationToken cancellationToken)
    {
        await _LessonScheduleDefinitionService.UpdateAsync(request.LessonScheduleDefinition);
        return new("updated");
    }
}
