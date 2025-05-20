using BritishTime.Application.Features.Students.Commands.CreateStudent;
using BritishTime.Domain.Dtos;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class StudentsController : ApiController
{
    public StudentsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentRequest request)
    {
        CreateStudentCommand command = new(request.Payment, request.Student);
        CreateStudentCommandResponse response = await _mediator.Send(command);

        return Ok(response);
    }
}