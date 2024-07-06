

namespace Store.Domain.Model
{
    public class StoreModel : EntityBase
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public ProductStatus Status { get; set; }
        public string StatusDescription
        {
            get
            {
                return Status.ToString();
            }
        }
    }
}
