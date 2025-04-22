using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.UpdateInstallmentSetting;

public sealed record UpdateInstallmentSettingCommand(InstallmentSettingDto InstallmentSetting) : IRequest<UpdateInstallmentSettingCommandResponse>;