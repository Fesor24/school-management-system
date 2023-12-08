namespace SMS.Domain.Primitives;
public abstract class AggregateRoot : BaseAuditableEntity
{
    protected AggregateRoot() { }

    protected AggregateRoot(Guid id) : base(id)
    {
        
    }
}
