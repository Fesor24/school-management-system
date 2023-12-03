using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Common;
public class BaseEntity
{
    public Guid Id { get; set; }

    private readonly List<IDomainEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvents(IDomainEvent baseEvent) =>
        _domainEvents.Add(baseEvent);

    public void DeleteDomainEvents(IDomainEvent baseEvent) => 
        _domainEvents.Remove(baseEvent);

    public void ClearDomainEvents() => 
        _domainEvents.Clear();
}
