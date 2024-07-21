using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Features.Category.Queries.GetCategory;
using Product.Api.Features.Product.Queries.GetProductList;

namespace Product.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProduct()
    {
        var request = new GetProductListQuery();
        var get = await _mediator.Send(request);
        return Ok(get);
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategory()
    {
        var request = new GetCategoryQuery();
        var getCate = await _mediator.Send(request);
        return Ok(getCate);
    }
    
}