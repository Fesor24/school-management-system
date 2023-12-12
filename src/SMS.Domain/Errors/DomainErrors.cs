using SMS.Domain.Shared;

namespace SMS.Domain.Errors;
public static class DomainErrors
{
    public static class Course
    {
        public static readonly Error InvalidCourseUnit = new (
            StatusCodes.BADREQUEST, 
            "Course unit must be greater than 0 and less than 7");

        public static readonly Error CourseNotFound = new(
            StatusCodes.NOTFOUND,
            "Course not found");
    }

    public static class Department
    {
        public static readonly Error DepartmentNotFound = new
            (StatusCodes.NOTFOUND, 
            "Department not found");
    }
}
