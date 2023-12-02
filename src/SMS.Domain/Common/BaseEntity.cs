using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Common;
public class BaseEntity
{
    public int Id { get; set; }

    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvents(BaseEvent baseEvent) =>
        _domainEvents.Add(baseEvent);

    public void DeleteDomainEvents(BaseEvent baseEvent) => 
        _domainEvents.Remove(baseEvent);

    public void ClearDomainEvents() => 
        _domainEvents.Clear();
}
