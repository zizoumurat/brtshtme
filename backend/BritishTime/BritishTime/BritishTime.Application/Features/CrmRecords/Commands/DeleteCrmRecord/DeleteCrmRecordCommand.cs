using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CrmRecords.Commands.DeleteCrmRecord;

public sealed record DeleteCrmRecordCommand(Guid Id) : IRequest<DeleteCrmRecordCommandResponse>;