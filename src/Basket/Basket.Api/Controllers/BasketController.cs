using Basket.Application.Features.Basket.Commands.Create;
using Basket.Application.Features.Basket.Queries.BasketGet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IMediator mediator, ILogger<BasketController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetBasket(string UserId)
        {
            var requset = await _mediator.Send(new GetBasketUserIdQuery(UserId));
            return Ok(requset);
        }

        [HttpPost]
        public async Task<IActionResult> PostBasket(CreateBasketCommand command, string userId)
        {
            command.UserId = userId; 

            var basketDto = await _mediator.Send(command); 

            return Ok(basketDto);
        }
    }
}
