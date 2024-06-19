using MediatR;
using Product.Application.Mapping;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Application.Features.Product.Commands.CreateProduct;

public class CreatProductHandler : IRequestHandler<CreateProductCommand, string>
{
    private readonly IProductRepository _productRepository;

    public CreatProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = ProductMapper.Mapper.Map<ProductDto>(request); 
        
        var addToProduct = await _productRepository.AddProduct(newProduct);

        return addToProduct;
    }
}