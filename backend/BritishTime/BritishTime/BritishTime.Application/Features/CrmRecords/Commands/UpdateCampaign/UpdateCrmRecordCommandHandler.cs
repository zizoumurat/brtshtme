using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecords.Commands.UpdateCrmRecord;

public class UpdateCrmRecordCommandHandler : IRequestHandler<UpdateCrmRecordCommand, UpdateCrmRecordCommandResponse>
{
    private readonly ICrmRecordService _CrmRecordService;

    public UpdateCrmRecordCommandHandler(ICrmRecordService CrmRecordService)
    {
        _CrmRecordService = CrmRecordService;
    }

    public async Task<UpdateCrmRecordCommandResponse> Handle(UpdateCrmRecordCommand request, CancellationToken cancellationToken)
    {
        await _CrmRecordService.UpdateAsync(request.CrmRecord);
        return new("updated");
    }
}
