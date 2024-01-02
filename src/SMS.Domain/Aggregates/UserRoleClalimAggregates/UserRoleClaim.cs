using Microsoft.AspNetCore.Identity;

namespace SMS.Domain.Aggregates.UserRoleClalimAggregates;
public sealed class UserRoleClaim : IdentityRoleClaim<Guid>
{
    public UserRoleClaim()
    {
        
    }

    public UserRoleClaim(string description, string group)
    {
        Description = description;
        Group = group;
    }

    public string Description { get; private set; }
    public string Group { get; private set; }
}
