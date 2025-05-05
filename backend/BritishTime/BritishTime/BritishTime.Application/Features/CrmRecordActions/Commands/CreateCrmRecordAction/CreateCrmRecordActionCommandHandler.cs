using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.CrmRecordActions.Commands.CreateCrmRecordAction;

public class CreateCrmRecordActionCommandHandler : IRequestHandler<CreateCrmRecordActionCommand, CreateCrmRecordActionCommandResponse>
{
    private readonly ICrmRecordActionService _CrmRecordActionService;

    public CreateCrmRecordActionCommandHandler(ICrmRecordActionService CrmRecordActionService)
    {
        _CrmRecordActionService = CrmRecordActionService;
    }

    public async Task<CreateCrmRecordActionCommandResponse> Handle(CreateCrmRecordActionCommand request, CancellationToken cancellationToken)
    {
        await _CrmRecordActionService.AddAsync(request.CrmRecordAction);
        return new("added");
    }
}
