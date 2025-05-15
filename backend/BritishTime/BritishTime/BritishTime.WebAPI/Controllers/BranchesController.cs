using BritishTime.Application.Features.BrancheFeatures.Queries.GetAllBranches;
using BritishTime.Application.Features.BrancheFeatures.Queries.GetUserBranches;
using BritishTime.Application.Features.Branches.Commands.CreateBranch;
using BritishTime.Application.Features.Branches.Commands.DeleteBranch;
using BritishTime.Application.Features.Branches.Commands.UpdateBranch;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class BranchesController : ApiController
{
    public BranchesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] BranchFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllBranchesQuery query = new(filter, pagination);
        GetAllBranchesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpGet("user-branch-list")]
    public async Task<IActionResult> GetAll()
    {
        GetUserBranchesQuery query = new();
        GetUserBranchesQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BranchCreateDto branch)
    {
        CreateBranchCommand request = new(branch);
        CreateBranchCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(BranchDto branch)
    {
        UpdateBranchCommand request = new(branch);
        UpdateBranchCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteBranchCommand request = new(id);
        DeleteBranchCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

}
