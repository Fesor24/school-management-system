using SMS.Domain.Exceptions.BaseExceptions;

namespace SMS.Domain.Exceptions.Department;
public class DepartmentNotFoundException : NotFoundException
{
    public DepartmentNotFoundException(Guid id) : base($"Department with Id: {id} not found")
    {
        
    }
}
