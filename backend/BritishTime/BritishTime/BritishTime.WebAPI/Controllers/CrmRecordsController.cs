using BritishTime.Application.Features.CrmRecords.Commands.CreateCrmRecord;
using BritishTime.Application.Features.CrmRecords.Commands.DeleteCrmRecord;
using BritishTime.Application.Features.CrmRecords.Commands.UpdateCrmRecord;
using BritishTime.Application.Features.CrmRecordsFeatures.Queries.CheckPhone;
using BritishTime.Application.Features.CrmRecordsFeatures.Queries.GetAllCrmRecords;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class CrmRecordsController : ApiController
{
    public CrmRecordsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CrmRecordFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllCrmRecordsQuery query = new(filter, pagination);
        GetAllCrmRecordsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("check-phone")]
    public async Task<IActionResult> GetAll([FromQuery] string phone)
    {
        CheckPhoneQuery query = new(phone);
        CheckPhoneQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CrmRecordCreateDto CrmRecord)
    {
        CreateCrmRecordCommand request = new(CrmRecord);
        CreateCrmRecordCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CrmRecordDto CrmRecord)
    {
        UpdateCrmRecordCommand request = new(CrmRecord);
        UpdateCrmRecordCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteCrmRecordCommand request = new(id);
        DeleteCrmRecordCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}