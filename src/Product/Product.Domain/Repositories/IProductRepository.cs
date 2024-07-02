using Product.Domain.Model.Dto;

namespace Product.Domain.Repositories;

public interface IProductRepository
{
    Task<string> AddProduct(ProductDto addNewProductDto);
    Task<List<ProductDto>> GetProductList();
    Task<ProductDto> GetProductById(Guid productId);
    Task<string> UpdateProduct(UpdateProductDto updateProduct);
    Task<bool> DeleteProduct(Guid productId);
}