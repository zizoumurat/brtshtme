using BritishTime.Application.Features.CourseSaleSettings.Commands.CreateCourseSaleSetting;
using BritishTime.Application.Features.CourseSaleSettings.Commands.DeleteCourseSaleSetting;
using BritishTime.Application.Features.CourseSaleSettings.Commands.UpdateCourseSaleSetting;
using BritishTime.Application.Features.CourseSaleSettingsFeatures.Queries.GetAllCourseSaleSettings;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class CourseSaleSettingsController : ApiController
{
    public CourseSaleSettingsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CourseSaleSettingFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllCourseSaleSettingsQuery query = new(filter, pagination);
        GetAllCourseSaleSettingsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseSaleSettingCreateDto CourseSaleSetting)
    {
        CreateCourseSaleSettingCommand request = new(CourseSaleSetting);
        CreateCourseSaleSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CourseSaleSettingDto CourseSaleSetting)
    {
        UpdateCourseSaleSettingCommand request = new(CourseSaleSetting);
        UpdateCourseSaleSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteCourseSaleSettingCommand request = new(id);
        DeleteCourseSaleSettingCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}