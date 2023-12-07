using SMS.Domain.Exceptions.BaseExceptions;

namespace SMS.Domain.Exceptions.Course;
public class CourseInvalidUnitException : BadRequestException
{
    public CourseInvalidUnitException(string message) : base(message)
    {
        
    }
}
