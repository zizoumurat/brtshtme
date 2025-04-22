using BritishTime.Application.Features.LessonScheduleDefinitiones.Commands.CreateLessonScheduleDefinition;
using BritishTime.Application.Features.LessonScheduleDefinitions.Commands.DeleteLessonScheduleDefinition;
using BritishTime.Application.Features.LessonScheduleDefinitions.Commands.UpdateLessonScheduleDefinition;
using BritishTime.Application.Features.LessonScheduleDefinitionsFeatures.Queries.GetAllLessonScheduleDefinitions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class LessonScheduleDefinitionsController : ApiController
{
    public LessonScheduleDefinitionsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] LessonScheduleDefinitionFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllLessonScheduleDefinitionsQuery query = new(filter, pagination);
        GetAllLessonScheduleDefinitionsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LessonScheduleDefinitionCreateDto lessonScheduleDefinition)
    {
        CreateLessonScheduleDefinitionCommand request = new(lessonScheduleDefinition);
        CreateLessonScheduleDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(LessonScheduleDefinitionCreateDto LessonScheduleDefinition)
    {
        UpdateLessonScheduleDefinitionCommand request = new(LessonScheduleDefinition);
        UpdateLessonScheduleDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteLessonScheduleDefinitionCommand request = new(id);
        DeleteLessonScheduleDefinitionCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}