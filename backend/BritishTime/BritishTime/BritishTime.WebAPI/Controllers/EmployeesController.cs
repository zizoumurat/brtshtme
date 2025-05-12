using BritishTime.Application.Features.Employees.Commands.CreateEmployee;
using BritishTime.Application.Features.Employees.Commands.DeleteEmployee;
using BritishTime.Application.Features.Employees.Commands.UpdateEmployee;
using BritishTime.Application.Features.EmployeesFeatures.Queries.GetAllEmployees;
using BritishTime.Application.Features.EmployeesFeatures.Queries.GetEmployeeList;
using BritishTime.Application.Features.RegionsFeatures.Queries.GetRegionList;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class EmployeesController : ApiController
{
    public EmployeesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] EmployeeFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllEmployeesQuery query = new(filter, pagination);
        GetAllEmployeesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }


    [HttpGet("select-list/{branchId}")]
    public async Task<IActionResult> GetSelectList([FromRoute] Guid BranchId)
    {
        GetEmployeeListQuery query = new(BranchId);
        GetEmployeeListQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeCreateDto Employee)
    {
        CreateEmployeeCommand request = new(Employee);
        CreateEmployeeCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeDto Employee)
    {
        UpdateEmployeeCommand request = new(Employee);
        UpdateEmployeeCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteEmployeeCommand request = new(id);
        DeleteEmployeeCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}