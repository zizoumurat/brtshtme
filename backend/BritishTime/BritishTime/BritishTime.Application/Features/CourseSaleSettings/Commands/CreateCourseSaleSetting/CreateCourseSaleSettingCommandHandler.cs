using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.CreateCourseSaleSetting;

public class CreateCourseSaleSettingCommandHandler : IRequestHandler<CreateCourseSaleSettingCommand, CreateCourseSaleSettingCommandResponse>
{
    private readonly ICourseSaleSettingService _CourseSaleSettingService;

    public CreateCourseSaleSettingCommandHandler(ICourseSaleSettingService CourseSaleSettingService)
    {
        _CourseSaleSettingService = CourseSaleSettingService;
    }

    public async Task<CreateCourseSaleSettingCommandResponse> Handle(CreateCourseSaleSettingCommand request, CancellationToken cancellationToken)
    {
        await _CourseSaleSettingService.AddAsync(request.CourseSaleSetting);
        return new("added");
    }
}
