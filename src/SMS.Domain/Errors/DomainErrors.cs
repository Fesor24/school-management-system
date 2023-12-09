using SMS.Domain.Shared;

namespace SMS.Domain.Errors;
public static class DomainErrors
{
    public static class Course
    {
        public static readonly Error InvalidCourseUnit = new (
            "Course.Unit", 
            "Course unit must be greater than 0 and less than 7");

        public static readonly Error CourseNotFound = new(
            "Course.NotFound",
            "Course not found");
    }
}
