using SMS.Domain.Primitives;

namespace SMS.Domain.DomainEvents.Course;
public record CourseDeletedEvent(Guid CourseId) : IDomainEvent;
