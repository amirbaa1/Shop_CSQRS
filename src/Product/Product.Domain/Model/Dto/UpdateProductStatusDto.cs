
namespace Product.Domain.Model.Dto
{
    public class UpdateProductStatusDto
    {
        public Guid ProductId { get; set; }
        public int Number { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
