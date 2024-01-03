namespace SMS.Domain.Primitives;
public class BaseAuditableEntity<TKey> : Entity<TKey> where TKey : IEquatable<TKey>
{
    protected BaseAuditableEntity() { }

    protected BaseAuditableEntity(TKey id) : base(id)
    {

    }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}
