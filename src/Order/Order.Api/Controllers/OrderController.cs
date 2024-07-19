using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Order.Application.Features.Order.Commands.Create;
using Order.Application.Features.Order.Queries.GetOrderByUserId;

namespace Order.Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Authorize(Policy = "orderUser")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IMediator mediator, ILogger<OrderController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderByUserId(string userId)
    {
        var order = await _mediator.Send(new GetOrderByUserIdQuery(userId));

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> PostOrder(CreateOrderCommand command)
    {
        _logger.LogInformation($"cc : -->{JsonConvert.SerializeObject(command)}");
        var postOrder = await _mediator.Send(command);
        return Ok(postOrder);
    }
}