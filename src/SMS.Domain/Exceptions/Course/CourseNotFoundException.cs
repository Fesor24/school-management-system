using SMS.Domain.Exceptions.BaseExceptions;

namespace SMS.Domain.Exceptions.Course;
public sealed class CourseNotFoundException : NotFoundException
{
    public CourseNotFoundException(Guid id) : base($"Course with Id: {id} not found")
    {
        
    }
}
