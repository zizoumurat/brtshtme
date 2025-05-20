using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.CreateCourseClass;

public class CreateCourseClassCommandHandler : IRequestHandler<CreateCourseClassCommand, CreateCourseClassCommandResponse>
{
    private readonly ICourseClassService _courseClassService;

    public CreateCourseClassCommandHandler(ICourseClassService courseClassService)
    {
        _courseClassService = courseClassService;
    }

    public async Task<CreateCourseClassCommandResponse> Handle(CreateCourseClassCommand request, CancellationToken cancellationToken)
    {
        await _courseClassService.AddAsync(request.CourseClass);
        return new("added");
    }
}
