using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CrmRecords.Commands.UpdateCrmRecord;

public sealed record UpdateCrmRecordCommand(CrmRecordDto CrmRecord) : IRequest<UpdateCrmRecordCommandResponse>;