namespace Order.Domain.Model;

public abstract class EntityBase
{
    public Guid Id { get; protected set; }
    public string CreateBy { get; set; }
    public DateTime CreateTime { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}