using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Category.Commands.Create;
using Product.Application.Features.Category.Commands.Delete;
using Product.Application.Features.Product.Commands.CreateProduct;
using Product.Application.Features.Product.Commands.Delete;
using Product.Application.Features.Product.Commands.Update.UpdateProduct;

namespace Product.Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
[Authorize(Policy = "productManagement")]
public class ProductManagementController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductManagementController(IMediator mediator)
    {
        _mediator = mediator;
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
        if (category == null)
        {
            return BadRequest("Category cannot be null");
        }

        var postCate = await _mediator.Send(category);
        return Ok(postCate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id)
    {
        var delete = await _mediator.Send(new DeleteCategoryCommand(id));
        return Ok(delete);
    }
}