using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Feature.Store.Commands.Create;
using Store.Application.Feature.Store.Commands.Delete;
using Store.Application.Feature.Store.Commands.Update.UpdateStoreNumber;
using Store.Application.Feature.Store.Queries.GetStore;

namespace Store.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStore()
        {
            var store = await _mediator.Send(new GetStoreQuery());

            return Ok(store);
        }

        [HttpPost]
        public async Task<IActionResult> PostStore([FromBody] CreateStoreCommand command)
        {

            var storeCreat = await _mediator.Send(command);
            return Ok(storeCreat);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStore([FromQuery] Guid productId)
        {
            var delete = await _mediator.Send(new DeleteStoreCommand(productId));
            return Ok(delete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStore([FromBody] UpdateStoreNumberCommand command)
        {
            var update = await _mediator.Send(command);
            return Ok(update);
        }
    }
}
