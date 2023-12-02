using SMS.Domain.Common;
using CourseEntity = SMS.Domain.Entities.Course;

namespace SMS.Domain.Events.Course;
public class CourseCreatedEvent : BaseEvent
{
    public CourseCreatedEvent(CourseEntity course)
    {
        Course = course;
    }

    public CourseEntity Course { get; set; }
}
