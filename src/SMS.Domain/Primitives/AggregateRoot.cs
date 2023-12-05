﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Primitives;
public abstract class AggregateRoot : BaseAuditableEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot() { }

    protected AggregateRoot(Guid id) : base(id)
    {
        
    }

    [NotMapped]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) => 
        _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() =>
        _domainEvents.Clear();
}
