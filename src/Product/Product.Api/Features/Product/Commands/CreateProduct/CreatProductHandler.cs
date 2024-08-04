using AutoMapper;
using MediatR;
using Product.Domain.Model.Dto;
using Product.Domain.Repositories;

namespace Product.Api.Features.Product.Commands.CreateProduct;

public class CreatProductHandler : IRequestHandler<CreateProductCommand, string>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreatProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = _mapper.Map<ProductDto>(request); 
        
        var addToProduct = await _productRepository.AddProduct(newProduct);

        return addToProduct;
    }
}