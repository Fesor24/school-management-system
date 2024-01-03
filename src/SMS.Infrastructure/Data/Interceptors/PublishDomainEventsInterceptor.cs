using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SMS.Domain.Primitives;

namespace SMS.Infrastructure.Data.Interceptors;
internal sealed class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public PublishDomainEventsInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();

        return base.SavedChanges(eventData, result);
    }

    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, 
        int result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEvents(eventData.Context);

        return result;
    }

    internal async Task PublishDomainEvents(DbContext? context)
    {
        if (context is null) return;

        var entities = context.ChangeTracker
            .Entries<Entity<Guid>>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity);

        var domainEvents = entities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        entities.ToList().ForEach(x => x.ClearDomainEvents());

        foreach(var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
