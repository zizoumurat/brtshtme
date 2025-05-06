using BritishTime.Application.Features.CampaignsFeatures.Queries.GetListByCrmRecord;
using BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidAppointmentsByDate;
using BritishTime.Application.Features.CampaignsFeatures.Queries.GetValidCallsByDate;
using BritishTime.Application.Features.CrmRecordActions.Commands.CreateCrmRecordAction;
using BritishTime.Domain.Dtos;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class CrmRecordActionsController : ApiController
{
    public CrmRecordActionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid CrmRecordId)
    {
        GetListByCrmRecordQuery query = new(CrmRecordId);
        GetListByCrmRecordQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("Appointments")]
    public async Task<IActionResult> GetAppointments([FromQuery] DateTime Date)
    {
        GetValidAppointmentsByDateQuery query = new(Date);
        GetValidAppointmentsByDateQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("Calls")]
    public async Task<IActionResult> GetCalls([FromQuery] DateTime Date)
    {
        GetValidCallsByDateQuery query = new(Date);
        GetValidCallsByDateQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CrmRecordActionCreateDto CrmRecordAction)
    {
        CreateCrmRecordActionCommand request = new(CrmRecordAction);
        CreateCrmRecordActionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

}