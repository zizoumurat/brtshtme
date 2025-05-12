using BritishTime.Application.Features.ClassRooms.Commands.CreateClassRoom;
using BritishTime.Application.Features.ClassRooms.Commands.DeleteClassRoom;
using BritishTime.Application.Features.ClassRooms.Commands.UpdateClassRoom;
using BritishTime.Application.Features.ClassRoomsFeatures.Queries.GetAllClassRooms;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class ClassRoomsController : ApiController
{
    public ClassRoomsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ClassRoomFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllClassRoomsQuery query = new(filter, pagination);
        GetAllClassRoomsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClassRoomCreateDto ClassRoom)
    {
        CreateClassRoomCommand request = new(ClassRoom);
        CreateClassRoomCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(ClassRoomDto ClassRoom)
    {
        UpdateClassRoomCommand request = new(ClassRoom);
        UpdateClassRoomCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteClassRoomCommand request = new(id);
        DeleteClassRoomCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}