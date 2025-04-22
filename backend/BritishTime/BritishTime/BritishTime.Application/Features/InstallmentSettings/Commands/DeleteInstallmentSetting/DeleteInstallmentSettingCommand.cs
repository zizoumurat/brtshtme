using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.DeleteInstallmentSetting;

public sealed record DeleteInstallmentSettingCommand(Guid Id) : IRequest<DeleteInstallmentSettingCommandResponse>;