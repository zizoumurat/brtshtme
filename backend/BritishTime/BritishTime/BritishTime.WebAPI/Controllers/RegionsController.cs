using BritishTime.Application.Features.Regions.Commands.CreateRegion;
using BritishTime.Application.Features.Regions.Commands.DeleteRegion;
using BritishTime.Application.Features.Regions.Commands.UpdateRegion;
using BritishTime.Application.Features.RegionsFeatures.Queries.GetAllRegions;
using BritishTime.Application.Features.RegionsFeatures.Queries.GetRegionList;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class RegionsController : ApiController
{
    public RegionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RegionFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllRegionsQuery query = new(filter, pagination);
        GetAllRegionsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("select-list")]
    public async Task<IActionResult> GetSelectList()
    {
        GetRegionListQuery query = new();
        GetRegionListQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(RegionCreateDto Region)
    {
        CreateRegionCommand request = new(Region);
        CreateRegionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(RegionDto Region)
    {
        UpdateRegionCommand request = new(Region);
        UpdateRegionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteRegionCommand request = new(id);
        DeleteRegionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}