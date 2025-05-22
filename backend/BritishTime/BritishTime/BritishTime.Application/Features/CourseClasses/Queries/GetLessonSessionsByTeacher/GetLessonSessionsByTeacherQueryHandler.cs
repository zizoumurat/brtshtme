
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetLessonSessionsByTeacher;

public sealed class GetLessonSessionsByTeacherQueryHandler : IRequestHandler<GetLessonSessionsByTeacherQuery, GetLessonSessionsByTeacherQueryResponse>
{
    private readonly ICourseClassService _courseClassService;

    public GetLessonSessionsByTeacherQueryHandler(ICourseClassService courseClasseservice)
    {
        _courseClassService = courseClasseservice;
    }
    public async Task<GetLessonSessionsByTeacherQueryResponse> Handle(GetLessonSessionsByTeacherQuery request, CancellationToken cancellationToken)
    {
        var result = await _courseClassService.GetLessonSessionListByTeacher(request.employeeId);

        return new(result);
    }
}