using BritishTime.Application.Features.InstallmentSettings.Commands.CreateInstallmentSetting;
using BritishTime.Application.Features.InstallmentSettings.Commands.DeleteInstallmentSetting;
using BritishTime.Application.Features.InstallmentSettings.Commands.UpdateInstallmentSetting;
using BritishTime.Application.Features.InstallmentSettingsFeatures.Queries.GetAllInstallmentSettings;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class InstallmentSettingsController : ApiController
{
    public InstallmentSettingsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] InstallmentSettingFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllInstallmentSettingsQuery query = new(filter, pagination);
        GetAllInstallmentSettingsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(InstallmentSettingCreateDto InstallmentSetting)
    {
        CreateInstallmentSettingCommand request = new(InstallmentSetting);
        CreateInstallmentSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(InstallmentSettingDto InstallmentSetting)
    {
        UpdateInstallmentSettingCommand request = new(InstallmentSetting);
        UpdateInstallmentSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteInstallmentSettingCommand request = new(id);
        DeleteInstallmentSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}