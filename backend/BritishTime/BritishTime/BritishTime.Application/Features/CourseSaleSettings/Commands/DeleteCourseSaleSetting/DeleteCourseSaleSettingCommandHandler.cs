using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.DeleteCourseSaleSetting;

public class DeleteCourseSaleSettingCommandHandler : IRequestHandler<DeleteCourseSaleSettingCommand, DeleteCourseSaleSettingCommandResponse>
{
    private readonly ICourseSaleSettingService _CourseSaleSettingService;

    public DeleteCourseSaleSettingCommandHandler(ICourseSaleSettingService CourseSaleSettingService)
    {
        _CourseSaleSettingService = CourseSaleSettingService;
    }

    public async Task<DeleteCourseSaleSettingCommandResponse> Handle(DeleteCourseSaleSettingCommand request, CancellationToken cancellationToken)
    {
        await _CourseSaleSettingService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
