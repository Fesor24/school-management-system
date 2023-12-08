using SMS.Domain.Primitives;

namespace SMS.Domain.DomainEvents.Course;
public sealed record CourseCreatedEvent(string CourseName, string CourseCode, int Unit) : IDomainEvent;
