
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidAppointmentsByDate;

public sealed class GetValidAppointmentsByDateQueryHandler : IRequestHandler<GetValidAppointmentsByDateQuery, GetValidAppointmentsByDateQueryResponse>
{
    private readonly ICrmRecordActionService _crmRecordActionService;

    public GetValidAppointmentsByDateQueryHandler(ICrmRecordActionService crmRecordActionService)
    {
        _crmRecordActionService = crmRecordActionService;
    }
    public async Task<GetValidAppointmentsByDateQueryResponse> Handle(GetValidAppointmentsByDateQuery request, CancellationToken cancellationToken)
    {
        var result = await _crmRecordActionService.GetValidAppointmentsByDateAsync(request.Date);

        return new(result);
    }
}