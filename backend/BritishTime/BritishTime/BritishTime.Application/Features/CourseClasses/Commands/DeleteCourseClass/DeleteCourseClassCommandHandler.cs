using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.DeleteCourseClass;

public class DeleteCourseClassCommandHandler : IRequestHandler<DeleteCourseClassCommand, DeleteCourseClassCommandResponse>
{
    private readonly ICourseClassService _courseClassService;

    public DeleteCourseClassCommandHandler(ICourseClassService courseClasseservice)
    {
        _courseClassService = courseClasseservice;
    }

    public async Task<DeleteCourseClassCommandResponse> Handle(DeleteCourseClassCommand request, CancellationToken cancellationToken)
    {
        await _courseClassService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
