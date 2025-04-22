using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.UpdateCourseSaleSetting;

public class UpdateCourseSaleSettingCommandHandler : IRequestHandler<UpdateCourseSaleSettingCommand, UpdateCourseSaleSettingCommandResponse>
{
    private readonly ICourseSaleSettingService _CourseSaleSettingService;

    public UpdateCourseSaleSettingCommandHandler(ICourseSaleSettingService CourseSaleSettingService)
    {
        _CourseSaleSettingService = CourseSaleSettingService;
    }

    public async Task<UpdateCourseSaleSettingCommandResponse> Handle(UpdateCourseSaleSettingCommand request, CancellationToken cancellationToken)
    {
        await _CourseSaleSettingService.UpdateAsync(request.CourseSaleSetting);
        return new("updated");
    }
}
