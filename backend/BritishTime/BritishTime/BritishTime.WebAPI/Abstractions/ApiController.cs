﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BritishTime.WebAPI.Abstractions;

[Route("api/[controller]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
    public readonly IMediator _mediator;
    protected ApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
