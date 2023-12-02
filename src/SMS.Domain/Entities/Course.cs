using SMS.Domain.Common;

namespace SMS.Domain.Entities;
public class Course : BaseAuditableEntity
{
    public Course()
    {
        
    }

    public Course(string courseName, string courseCode)
    {
        CourseName = courseName;
        CourseCode = courseCode;
    }

    public string CourseName { get; private set; }
    public string CourseCode { get; private set; }
}
