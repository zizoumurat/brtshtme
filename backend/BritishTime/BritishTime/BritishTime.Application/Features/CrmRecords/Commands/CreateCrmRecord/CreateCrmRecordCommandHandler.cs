using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecords.Commands.CreateCrmRecord;

public class CreateCrmRecordCommandHandler : IRequestHandler<CreateCrmRecordCommand, CreateCrmRecordCommandResponse>
{
    private readonly ICrmRecordService _CrmRecordService;

    public CreateCrmRecordCommandHandler(ICrmRecordService CrmRecordService)
    {
        _CrmRecordService = CrmRecordService;
    }

    public async Task<CreateCrmRecordCommandResponse> Handle(CreateCrmRecordCommand request, CancellationToken cancellationToken)
    {
        var added = await _CrmRecordService.AddAsync(request.CrmRecord);
        return new(added.Id);
    }
}
