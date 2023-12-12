using SMS.Application.Courses.Response;

namespace SMS.Application.Department.Response;
public record GetDepartmentResponse(
    Guid Id, 
    string Name, 
    string Code);
