using BritishTime.Application.Features.Levels.Commands.CreateLevel;
using BritishTime.Application.Features.Levels.Commands.DeleteLevel;
using BritishTime.Application.Features.Levels.Commands.UpdateLevel;
using BritishTime.Application.Features.LevelsFeatures.Queries.GetAllLevels;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class LevelsController : ApiController
{
    public LevelsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] LevelFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllLevelsQuery query = new(filter, pagination);
        GetAllLevelsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LevelCreateDto Level)
    {
        CreateLevelCommand request = new(Level);
        CreateLevelCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(LevelDto Level)
    {
        UpdateLevelCommand request = new(Level);
        UpdateLevelCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteLevelCommand request = new(id);
        DeleteLevelCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}