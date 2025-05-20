
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Queries.GetAllCourseClasses;

public sealed class GetAllCourseClassesQueryHandler : IRequestHandler<GetAllCourseClassesQuery, GetAllCourseClassesQueryResponse>
{
    private readonly ICourseClassService _courseClassService;

    public GetAllCourseClassesQueryHandler(ICourseClassService courseClasseservice)
    {
        _courseClassService = courseClasseservice;
    }
    public async Task<GetAllCourseClassesQueryResponse> Handle(GetAllCourseClassesQuery request, CancellationToken cancellationToken)
    {
        var result = await _courseClassService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}