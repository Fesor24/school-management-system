using SMS.Domain.Shared;

namespace SMS.Domain.Errors;
public static class DomainErrors
{
    public static class Course
    {
        public static readonly Error InvalidCourseUnit = Error.Validation(
            "COURSE.INVALIDUNIT", 
            "Course unit must be greater than 0 and less than 7");

        public static Error CourseNotFound(Guid courseId) => Error.NotFound(
            "COURSE.NOTFOUND",
            $"Course with Id: '{courseId}' not found");
    }

    public static class Department
    {
        public static Error DepartmentNotFound(Guid departmentId) => Error.NotFound
            ("DEPARTMENT.NOTFOUND", 
            $"Department with Id: '{departmentId}' not found");

        public static Error DepartmentBadRequest(params string[] data) => Error.Validation(
            "INVALID.INPUT",
            $"Ensure validity of data passed: {data}"
            );
    }
}
