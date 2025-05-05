using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecords.Commands.DeleteCrmRecord;

public class DeleteCrmRecordCommandHandler : IRequestHandler<DeleteCrmRecordCommand, DeleteCrmRecordCommandResponse>
{
    private readonly ICrmRecordService _CrmRecordService;

    public DeleteCrmRecordCommandHandler(ICrmRecordService CrmRecordService)
    {
        _CrmRecordService = CrmRecordService;
    }

    public async Task<DeleteCrmRecordCommandResponse> Handle(DeleteCrmRecordCommand request, CancellationToken cancellationToken)
    {
        await _CrmRecordService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
