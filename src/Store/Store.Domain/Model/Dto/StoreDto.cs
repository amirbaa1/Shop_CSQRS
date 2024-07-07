namespace Store.Domain.Model.Dto
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTimeStatus { get; set; }
        public DateTime LastUpdateTimeProduct { get; set; }
    }
}