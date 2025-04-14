using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.IncentiveSettinges.Commands.CreateIncentiveSetting;

public sealed record CreateIncentiveSettingCommand(IncentiveSettingCreateDto IncentiveSetting) : IRequest<CreateIncentiveSettingCommandResponse>;