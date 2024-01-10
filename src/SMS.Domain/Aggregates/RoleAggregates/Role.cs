using Microsoft.AspNetCore.Identity;

namespace SMS.Domain.Aggregates.UserRoleAggregates;
public sealed class Role : IdentityRole<Guid>
{
    public Role()
    {
        
    }

    public Role(string name, string description) : base(name)
    {
        Description = description;
    }
    public string Description { get; private set; }
}