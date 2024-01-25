using Microsoft.AspNetCore.Authorization;
using SMS.Shared.Authorization;

namespace SMS.API.Permissions;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        if (context.User is null)
        {
            context.Fail();
            return Task.CompletedTask;
        }
            
        var claims = context.User!.Claims
            .Where(x => x.Type == AppClaim.Permission 
            && x.Value == requirement.Permission
            && x.Issuer == "LOCAL AUTHORITY");

        if (claims.Any())
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}
