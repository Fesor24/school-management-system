using SMS.Application.Common.Response;

namespace SMS.Application.Department.Response;
public record CreateDepartmentResponse(
    Guid Id, 
    string Name, 
    string Code) : CreateResponse(Id);
