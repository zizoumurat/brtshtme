using BritishTime.Application.Features.Campaigns.Commands.CreateCampaign;
using BritishTime.Application.Features.Campaigns.Commands.DeleteCampaign;
using BritishTime.Application.Features.Campaigns.Commands.UpdateCampaign;
using BritishTime.Application.Features.CampaignsFeatures.Queries.GetAllCampaigns;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class CampaignsController : ApiController
{
    public CampaignsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CampaignFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllCampaignsQuery query = new(filter, pagination);
        GetAllCampaignsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CampaignCreateDto Campaign)
    {
        CreateCampaignCommand request = new(Campaign);
        CreateCampaignCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CampaignDto Campaign)
    {
        UpdateCampaignCommand request = new(Campaign);
        UpdateCampaignCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteCampaignCommand request = new(id);
        DeleteCampaignCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}