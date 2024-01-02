using Microsoft.AspNetCore.Identity;
using SMS.Domain.Primitives;

namespace SMS.Domain.Aggregates.UserRoleAggregates;
public sealed class UserRole : IdentityRole<Guid>, IEntity
{
    public UserRole()
    {
        
    }

    public UserRole(string description, string name) : base(name)
    {
        Description = description;
    }
    public string Description { get; private set; }
}