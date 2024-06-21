using Basket.Application.Features.Basket.Commands.Create;
using Basket.Application.Features.Basket.Commands.Delete;
using Basket.Application.Features.Basket.Commands.Update;
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

        [HttpPut]
        public async Task<IActionResult> UpdateBasket(Guid BasketItemId, int Quantity)
        {
            var updateBasket = await _mediator.Send(new UpdateBasketCommand(BasketItemId, Quantity));

            return Ok(updateBasket);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(Guid basketId)
        {
            var DeleteBasketItem = await _mediator.Send(new DeleteBasketCommand(basketId));

            return Ok(DeleteBasketItem);
        }
    }
}
