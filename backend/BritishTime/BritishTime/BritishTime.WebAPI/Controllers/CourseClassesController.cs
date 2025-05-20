using BritishTime.Application.Features.CourseClasses.Commands.CreateCourseClass;
using BritishTime.Application.Features.CourseClasses.Commands.DeleteCourseClass;
using BritishTime.Application.Features.CourseClasses.Commands.UpdateCourseClass;
using BritishTime.Application.Features.CourseClasses.Queries.GetAllCourseClasses;
using BritishTime.Application.Features.CourseClasses.Queries.GetEndDate;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class CourseClassesController : ApiController
{
    public CourseClassesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CourseClassFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllCourseClassesQuery query = new(filter, pagination);
        GetAllCourseClassesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost("calculate-end-date")]
    public async Task<IActionResult> GetAll(CourseEndDateRequest request)
    {
        GetEndDateQuery query = new(request);
        GetEndDateQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseClassCreateDto CourseClass)
    {
        CreateCourseClassCommand request = new(CourseClass);
        CreateCourseClassCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CourseClassDto CourseClass)
    {
        UpdateCourseClassCommand request = new(CourseClass);
        UpdateCourseClassCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteCourseClassCommand request = new(id);
        DeleteCourseClassCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}