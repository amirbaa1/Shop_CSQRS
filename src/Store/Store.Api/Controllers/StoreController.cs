﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Feature.Store.Commands.Create;
using Store.Api.Feature.Store.Commands.Delete;
using Store.Api.Feature.Store.Commands.Update.UpdateProductName;
using Store.Api.Feature.Store.Commands.Update.UpdateProductStatus;
using Store.Api.Feature.Store.Commands.Update.UpdateStoreNumber;
using Store.Api.Feature.Store.Queries.GetStore;
namespace Store.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    // [Authorize(Policy = "storeManagement")]
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

        [HttpPut("UpdateInventory")]
        public async Task<IActionResult> UpdateInventory([FromBody] UpdateStoreNumberCommand command)
        {
            var update = await _mediator.Send(command);
            return Ok(update);
        }

        [HttpPut("UpdateProductStatus")]
        public async Task<IActionResult> UpdateProductStatus([FromBody] UpdateProductStatusCommand command)
        {
            var update = await _mediator.Send(command);
            return Ok(update);
        }

        [HttpPut("UpdateProductName")]
        public async Task<IActionResult> UpdateProductName([FromBody] UpdateProductNameCommand command)
        {
            var update = await _mediator.Send(command);
            return Ok(update);
        }
    }
}