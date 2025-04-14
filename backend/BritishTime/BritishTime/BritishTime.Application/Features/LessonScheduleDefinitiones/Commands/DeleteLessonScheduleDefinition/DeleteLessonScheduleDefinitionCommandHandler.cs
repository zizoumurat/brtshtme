using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.LessonScheduleDefinitions.Commands.DeleteLessonScheduleDefinition;

public class DeleteLessonScheduleDefinitionCommandHandler : IRequestHandler<DeleteLessonScheduleDefinitionCommand, DeleteLessonScheduleDefinitionCommandResponse>
{
    private readonly ILessonScheduleDefinitionService _LessonScheduleDefinitionService;

    public DeleteLessonScheduleDefinitionCommandHandler(ILessonScheduleDefinitionService LessonScheduleDefinitionService)
    {
        _LessonScheduleDefinitionService = LessonScheduleDefinitionService;
    }

    public async Task<DeleteLessonScheduleDefinitionCommandResponse> Handle(DeleteLessonScheduleDefinitionCommand request, CancellationToken cancellationToken)
    {
        await _LessonScheduleDefinitionService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
