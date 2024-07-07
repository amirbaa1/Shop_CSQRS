using Product.Domain.Model.Dto;

namespace Product.Domain.Repositories;

public interface IProductRepository
{
    Task<string> AddProduct(ProductDto addNewProduct);
    Task<List<ProductDto>> GetProductList();
    Task<ProductDto> GetProductById(Guid productId);
    Task<string> UpdateProduct(UpdateProductDto updateProduct);
    Task<string> UpdateProductStatus(UpdateProductStatusDto updateProductStatusDto);
    Task<bool> DeleteProduct(Guid productId);
}