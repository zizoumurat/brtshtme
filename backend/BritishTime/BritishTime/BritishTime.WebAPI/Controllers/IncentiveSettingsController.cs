using BritishTime.Application.Features.IncentiveSettinges.Commands.CreateIncentiveSetting;
using BritishTime.Application.Features.IncentiveSettings.Commands.DeleteIncentiveSetting;
using BritishTime.Application.Features.IncentiveSettings.Commands.UpdateIncentiveSetting;
using BritishTime.Application.Features.IncentiveSettingsFeatures.Queries.GetAllIncentiveSettings;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class IncentiveSettingsController : ApiController
{
    public IncentiveSettingsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] IncentiveSettingFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllIncentiveSettingsQuery query = new(filter, pagination);
        GetAllIncentiveSettingsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(IncentiveSettingCreateDto IncentiveSetting)
    {
        CreateIncentiveSettingCommand request = new(IncentiveSetting);
        CreateIncentiveSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(IncentiveSettingDto IncentiveSetting)
    {
        UpdateIncentiveSettingCommand request = new(IncentiveSetting);
        UpdateIncentiveSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteIncentiveSettingCommand request = new(id);
        DeleteIncentiveSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}