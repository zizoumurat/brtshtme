
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CourseSaleSettingsFeatures.Queries.GetAllCourseSaleSettings;

public sealed class GetAllCourseSaleSettingsQueryHandler : IRequestHandler<GetAllCourseSaleSettingsQuery, GetAllCourseSaleSettingsQueryResponse>
{
    private readonly ICourseSaleSettingService _CourseSaleSettingService;

    public GetAllCourseSaleSettingsQueryHandler(ICourseSaleSettingService CourseSaleSettingService)
    {
        _CourseSaleSettingService = CourseSaleSettingService;
    }
    public async Task<GetAllCourseSaleSettingsQueryResponse> Handle(GetAllCourseSaleSettingsQuery request, CancellationToken cancellationToken)
    {
        var result = await _CourseSaleSettingService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}