﻿using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.BranchPricingSettings.Commands.CreateBranchPricingSetting;

public sealed record CreateBranchPricingSettingCommand(BranchPricingSettingDto BranchPricingSetting) : IRequest<CreateBranchPricingSettingCommandResponse>;