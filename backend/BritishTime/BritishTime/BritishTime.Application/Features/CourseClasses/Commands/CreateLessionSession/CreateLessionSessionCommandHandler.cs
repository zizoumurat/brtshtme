using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.CreateLessionSession;

public class CreateLessionSessionCommandHandler : IRequestHandler<CreateLessionSessionCommand, CreateLessionSessionCommandResponse>
{
    private readonly ICourseClassService _courseClassService;

    public CreateLessionSessionCommandHandler(ICourseClassService courseClassService)
    {
        _courseClassService = courseClassService;
    }

    public async Task<CreateLessionSessionCommandResponse> Handle(CreateLessionSessionCommand request, CancellationToken cancellationToken)
    {
        await _courseClassService.GenerateLessonSessionAsync(request.request.classId, request.request.programDaysWithTeachers);
        return new("added");
    }
}
