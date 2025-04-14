using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.IncentiveSettings.Commands.UpdateIncentiveSetting;

public sealed record UpdateIncentiveSettingCommand(IncentiveSettingDto IncentiveSetting) : IRequest<UpdateIncentiveSettingCommandResponse>;