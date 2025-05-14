using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Dtos;
using BritishTime.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
public class SalesController : ApiController
{
    private readonly ISalesService _salesService;
    public SalesController(IMediator mediator, ISalesService salesService) : base(mediator)
    {
        _salesService = salesService;
    }

    [HttpPost("calculater")]
    public async Task<IActionResult> Index(CalculatePaymentDto model)
    {
        var response = await _salesService.CalculatePayment(model);

        return Ok(response);
    }
}
