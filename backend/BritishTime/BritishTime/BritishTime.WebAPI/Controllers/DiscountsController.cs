using BritishTime.Application.Features.Discounts.Commands.CreateDiscount;
using BritishTime.Application.Features.Discounts.Commands.DeleteDiscount;
using BritishTime.Application.Features.Discounts.Commands.UpdateDiscount;
using BritishTime.Application.Features.DiscountsFeatures.Queries.GetAllDiscounts;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public sealed class DiscountsController : ApiController
{
    public DiscountsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DiscountFilterDto filter, [FromQuery] PageRequest pagination)
    {
        GetAllDiscountsQuery query = new(filter, pagination);
        GetAllDiscountsQueryResponse response = await _mediator.Send(query);

        return Ok(response.result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DiscountCreateDto Discount)
    {
        CreateDiscountCommand request = new(Discount);
        CreateDiscountCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(DiscountDto Discount)
    {
        UpdateDiscountCommand request = new(Discount);
        UpdateDiscountCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeleteDiscountCommand request = new(id);
        DeleteDiscountCommandResponse response = await _mediator.Send(request);

        return Ok(response);
    }
}