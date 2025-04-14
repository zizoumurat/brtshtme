using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.IncentiveSettings.Commands.DeleteIncentiveSetting;

public sealed record DeleteIncentiveSettingCommand(Guid Id) : IRequest<DeleteIncentiveSettingCommandResponse>;