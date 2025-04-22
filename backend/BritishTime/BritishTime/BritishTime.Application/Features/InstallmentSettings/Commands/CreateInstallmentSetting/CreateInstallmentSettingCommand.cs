﻿using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.InstallmentSettings.Commands.CreateInstallmentSetting;

public sealed record CreateInstallmentSettingCommand(InstallmentSettingCreateDto InstallmentSetting) : IRequest<CreateInstallmentSettingCommandResponse>;