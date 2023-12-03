using SMS.Domain.Primitives;

namespace SMS.Domain.Common;
public class BaseAuditableEntity : Entity
{
    public BaseAuditableEntity(Guid id) : base(id)
    {
        
    }
    public DateTimeOffset CreatedAt { get;set; }
    public string CreatedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}
