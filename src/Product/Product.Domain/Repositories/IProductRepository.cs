using Product.Domain.Model.Dto;

namespace Product.Domain.Repositories;

public interface IProductRepository
{
    Task<string> AddProduct(ProductDto addNewProductDto);
    Task<List<ProductDto>> GetProductList();
    Task<ProductDto> GetProductById(Guid productId);
    Task<string> UpdateProductName(UpdateProductDto updateProduct);
    Task<bool> DeleteProduct(Guid productId);
}