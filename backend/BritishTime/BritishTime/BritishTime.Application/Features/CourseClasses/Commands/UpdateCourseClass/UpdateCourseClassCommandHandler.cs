using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.UpdateCourseClass;

public class UpdateCourseClassCommandHandler : IRequestHandler<UpdateCourseClassCommand, UpdateCourseClassCommandResponse>
{
    private readonly ICourseClassService _courseClassService;

    public UpdateCourseClassCommandHandler(ICourseClassService courseClasseservice)
    {
        _courseClassService = courseClasseservice;
    }

    public async Task<UpdateCourseClassCommandResponse> Handle(UpdateCourseClassCommand request, CancellationToken cancellationToken)
    {
        await _courseClassService.UpdateAsync(request.CourseClass);
        return new("updated");
    }
}
