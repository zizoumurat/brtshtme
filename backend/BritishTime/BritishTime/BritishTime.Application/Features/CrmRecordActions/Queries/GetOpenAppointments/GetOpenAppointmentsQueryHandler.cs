
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CampaignsFeatures.Queries.GetOpenAppointments;

public sealed class GetOpenAppointmentsQueryHandler : IRequestHandler<GetOpenAppointmentsQuery, GetOpenAppointmentsQueryResponse>
{
    private readonly ICrmRecordActionService _crmRecordActionService;

    public GetOpenAppointmentsQueryHandler(ICrmRecordActionService crmRecordActionService)
    {
        _crmRecordActionService = crmRecordActionService;
    }
    public async Task<GetOpenAppointmentsQueryResponse> Handle(GetOpenAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _crmRecordActionService.GetOpenAppointmentsAsync();

        return new(result);
    }
}