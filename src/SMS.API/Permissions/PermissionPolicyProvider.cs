using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SMS.Shared.Authorization;

namespace SMS.API.Permissions;

public class PermissionPolicyProvider : IAuthorizationPolicyProvider
{
    public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

    public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
        FallbackPolicyProvider.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
        Task.FromResult<AuthorizationPolicy?>(null);

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if(policyName.StartsWith(AppClaim.Permission, StringComparison.CurrentCultureIgnoreCase))
        {
            var policyBuilder = new AuthorizationPolicyBuilder();

            policyBuilder.AddRequirements(new PermissionRequirement(policyName));

            return Task.FromResult(policyBuilder.Build());
        }

        return FallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}
