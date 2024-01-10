using Microsoft.AspNetCore.Identity;

namespace SMS.Domain.Aggregates.UserRoleClalimAggregates;
public sealed class UserRoleClaim : IdentityRoleClaim<Guid>
{
    public UserRoleClaim()
    {
        
    }

    public UserRoleClaim(Guid roleId, string claimType, string claimValue, 
        string description, string group)
    {
        Description = description;
        Group = group;
        RoleId = roleId;
        ClaimType = claimType;
        ClaimValue = claimValue;
    }

    public string Description { get; private set; }
    public string Group { get; private set; }
}
