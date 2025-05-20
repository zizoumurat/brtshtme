
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetEndDate;

public sealed class GetEndDateQueryHandler : IRequestHandler<GetEndDateQuery, GetEndDateQueryResponse>
{
    private readonly ICourseClassService _courseClassService;

    public GetEndDateQueryHandler(ICourseClassService courseClasseservice)
    {
        _courseClassService = courseClasseservice;
    }
    public async Task<GetEndDateQueryResponse> Handle(GetEndDateQuery request, CancellationToken cancellationToken)
    {
        var result = await _courseClassService.CalculateEndDate(request.request.StartDate, request.request.LessonScheduleId);

        return new(result);
    }
}