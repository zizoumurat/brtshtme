
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.LessonScheduleDefinitionsFeatures.Queries.GetAllLessonScheduleDefinitions;

public sealed class GetAllLessonScheduleDefinitionsQueryHandler : IRequestHandler<GetAllLessonScheduleDefinitionsQuery, GetAllLessonScheduleDefinitionsQueryResponse>
{
    private readonly ILessonScheduleDefinitionService _LessonScheduleDefinitionService;

    public GetAllLessonScheduleDefinitionsQueryHandler(ILessonScheduleDefinitionService LessonScheduleDefinitionService)
    {
        _LessonScheduleDefinitionService = LessonScheduleDefinitionService;
    }
    public async Task<GetAllLessonScheduleDefinitionsQueryResponse> Handle(GetAllLessonScheduleDefinitionsQuery request, CancellationToken cancellationToken)
    {
        var result = await _LessonScheduleDefinitionService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}