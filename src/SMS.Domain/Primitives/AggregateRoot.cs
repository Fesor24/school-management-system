namespace SMS.Domain.Primitives;
public abstract class AggregateRoot<TKey> : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
{
    protected AggregateRoot() { }

    protected AggregateRoot(TKey id) : base(id)
    {
        
    }
}
