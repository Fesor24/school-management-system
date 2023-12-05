using SMS.Domain.Primitives;

namespace SMS.Domain.DomainEvents;
public sealed record CourseCreatedEvent(string CourseName, string CourseCode, int Unit) : IDomainEvent;
