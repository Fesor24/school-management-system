namespace SMS.Domain.Primitives;
public class BaseAuditableEntity : Entity
{
    protected BaseAuditableEntity() { }

    protected BaseAuditableEntity(Guid id) : base(id)
    {

    }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
