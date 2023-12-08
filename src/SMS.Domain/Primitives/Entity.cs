using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Primitives;
public abstract class Entity : IEquatable<Entity>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected Entity()
    {

    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; private init; }

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(IDomainEvent domainEvent) => 
        _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();

    public static bool operator ==(Entity? first, Entity? second)
    {
        return first is not null && second is not null && first.Equals(second);
    }

    public static bool operator !=(Entity? first, Entity? second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        if (obj is not Entity entity) return false;

        return entity.Id == Id;
    }

    public bool Equals(Entity? other)
    {
        if (other is null) return false;

        if (other.GetType() != GetType()) return false;

        return other.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
