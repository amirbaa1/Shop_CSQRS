using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Features.Order.Queries.GetAll;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //[Authorize("orderManagement")]
    public class OrderManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var response = new GetAllOrderQuery();
            var result = _mediator.Send(response);
            return Ok(result);
        }

    }
}
