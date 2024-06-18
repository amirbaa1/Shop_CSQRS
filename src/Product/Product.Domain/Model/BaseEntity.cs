namespace Product.Domain.Model;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDateTime { get; set; }
    public DateTime ModificationDateTime { get; set; }
}