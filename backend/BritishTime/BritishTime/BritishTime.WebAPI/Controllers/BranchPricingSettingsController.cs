using BritishTime.Application.Features.BranchPricingSettings.Commands.CreateBranchPricingSetting;
using BritishTime.Application.Features.BranchPricingSettings.Queries.GetBranchPricingSetting;
using BritishTime.Domain.Dtos;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class BranchPricingSettingsController : ApiController
{
    public BranchPricingSettingsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetByBranchId([FromQuery] Guid BranchId)
    {
        GetBranchPricingSettingQuery query = new(BranchId);
        GetBranchPricingSettingQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BranchPricingSettingDto BranchPricingSetting)
    {
        CreateBranchPricingSettingCommand request = new(BranchPricingSetting);
        CreateBranchPricingSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}