using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.LessonScheduleDefinitiones.Commands.CreateLessonScheduleDefinition;

public class CreateLessonScheduleDefinitionCommandHandler : IRequestHandler<CreateLessonScheduleDefinitionCommand, CreateLessonScheduleDefinitionCommandResponse>
{
    private readonly ILessonScheduleDefinitionService _LessonScheduleDefinitionService;

    public CreateLessonScheduleDefinitionCommandHandler(ILessonScheduleDefinitionService LessonScheduleDefinitionService)
    {
        _LessonScheduleDefinitionService = LessonScheduleDefinitionService;
    }

    public async Task<CreateLessonScheduleDefinitionCommandResponse> Handle(CreateLessonScheduleDefinitionCommand request, CancellationToken cancellationToken)
    {
        await _LessonScheduleDefinitionService.AddAsync(request.LessonScheduleDefinition);
        return new("added");
    }
}
