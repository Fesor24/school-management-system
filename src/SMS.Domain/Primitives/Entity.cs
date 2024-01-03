using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Primitives;
public abstract class Entity<TKey> : IEquatable<Entity<TKey>> where TKey : IEquatable<TKey>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity()
    {

    }

    protected Entity(TKey id)
    {
        Id = id;
    }

    public virtual TKey Id { get; private init; }

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(IDomainEvent domainEvent) => 
        _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();

    public static bool operator ==(Entity<TKey>? first, Entity<TKey>? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity<TKey>? first, Entity<TKey>? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        if (obj is not Entity<TKey> entity) return false;

        return Equals(entity);
    }

    public bool Equals(Entity<TKey>? other)
    {
        if (other is null) return false;

        if (other.GetType() != GetType()) return false;

        //if (other.Id == default || Id == default) return false;

        return other.Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}
