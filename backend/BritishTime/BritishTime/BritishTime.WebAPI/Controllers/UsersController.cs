using BritishTime.Application.Features.AppUsers.Commands.CreateAppUser;
using BritishTime.Application.Features.AppUsers.Commands.DeleteAppUser;
using BritishTime.Application.Features.AppUsersFeatures.Queries.GetAllAppUsers;
using BritishTime.Application.Features.AppUsersFeatures.Queries.GetUnassignedEmployees;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] AppUserFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllAppUsersQuery query = new(filter, pagination);
        GetAllAppUsersQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("unassigned-employees/{id}")]
    public async Task<IActionResult> GetUnassignedAppUsers([FromRoute] Guid id)
    {
        GetUnassignedEmployeesQuery query = new(id);
        GetUnassignedEmployeesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AppUserCreateDto AppUsers)
    {
        CreateAppUserCommand request = new(AppUsers);
        CreateAppUserCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteAppUserCommand request = new(id);
        DeleteAppUserCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}