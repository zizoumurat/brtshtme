using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CrmRecordActions.Commands.CreateCrmRecordAction;

public sealed record CreateCrmRecordActionCommand(CrmRecordActionCreateDto CrmRecordAction) : IRequest<CreateCrmRecordActionCommandResponse>;