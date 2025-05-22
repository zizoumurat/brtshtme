
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetLessonSessions;

public sealed class GetLessonSessionsQueryHandler : IRequestHandler<GetLessonSessionsQuery, GetLessonSessionsQueryResponse>
{
    private readonly ICourseClassService _courseClassService;

    public GetLessonSessionsQueryHandler(ICourseClassService courseClasseservice)
    {
        _courseClassService = courseClasseservice;
    }
    public async Task<GetLessonSessionsQueryResponse> Handle(GetLessonSessionsQuery request, CancellationToken cancellationToken)
    {
        var result = await _courseClassService.GetLessonSessionListByCourseClass(request.courseClassId);

        return new(result);
    }
}