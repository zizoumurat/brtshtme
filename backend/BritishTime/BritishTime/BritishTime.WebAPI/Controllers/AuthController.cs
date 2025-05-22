using BritishTime.Application.Features.Auth.Login;
using BritishTime.Application.Services.Abstract;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;
[AllowAnonymous]
public sealed class AuthController : ApiController
{
    private readonly IUserContextService _userContextService;
    public AuthController(IMediator mediator, IUserContextService userContextService) : base(mediator)
    {
        _userContextService = userContextService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("GetRoleList")]
    public async Task<IActionResult> GetUserRoleList()
    {
        var user = await _userContextService.GetCurrentUserAsync();
        var userRoleList = await _userContextService.GetUserRolesAsync(user);

        return Ok(userRoleList);
    }
}
