using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Category.Commands.Create;
using Product.Application.Features.Category.Commands.Delete;
using Product.Application.Features.Category.Queries.GetCategory;
using Product.Application.Features.Product.Commands.CreateProduct;
using Product.Application.Features.Product.Commands.Delete;
using Product.Application.Features.Product.Commands.Update;
using Product.Application.Features.Queries.GetProductList;

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

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] CreateProductCommand product)
    {
        var postProduct = await _mediator.Send(product);
        return Ok(postProduct);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
    {
        var updateProduct = await _mediator.Send(command);
        return Ok(updateProduct);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommand command)
    {
        var delete = await _mediator.Send(command);
        return Ok(delete);
    }

    [HttpPost("category")]
    public async Task<IActionResult> PostCategory([FromBody] CreateCategoryCommand category)
    {
        var postCate = await _mediator.Send(category);
        return Ok(postCate);
    }

    [HttpGet("category")]
    public async Task<IActionResult> GetCategory()
    {
        var request = new GetCategoryQuery();
        var getCate = await _mediator.Send(request);
        return Ok(getCate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var delete = await _mediator.Send(new DeleteCategoryCommand(id));
        return Ok(delete);
    }
}