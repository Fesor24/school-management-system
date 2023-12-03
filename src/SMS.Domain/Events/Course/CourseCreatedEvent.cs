using SMS.Domain.Common;
using CourseEntity = SMS.Domain.Entities.Course;

namespace SMS.Domain.Events.Course;
public class CourseCreatedEvent : IDomainEvent
{
    public CourseCreatedEvent(CourseEntity course)
    {
        Course = course;
    }

    public CourseEntity Course { get; set; }
}
