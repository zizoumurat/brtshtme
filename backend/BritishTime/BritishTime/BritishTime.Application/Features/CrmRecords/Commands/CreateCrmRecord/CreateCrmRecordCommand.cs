using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CrmRecords.Commands.CreateCrmRecord;

public sealed record CreateCrmRecordCommand(CrmRecordCreateDto CrmRecord) : IRequest<CreateCrmRecordCommandResponse>;