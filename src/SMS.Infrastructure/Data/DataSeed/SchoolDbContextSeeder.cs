using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SMS.Domain.Aggregates.UserAggregates;
using SMS.Domain.Aggregates.UserRoleAggregates;
using SMS.Domain.Aggregates.UserRoleClalimAggregates;
using SMS.Shared.Authorization;

namespace SMS.Infrastructure.Data.DataSeed;
public class SchoolDbContextSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SchoolDbContext _context;

    public SchoolDbContextSeeder(UserManager<User> userManager, RoleManager<Role> roleManager, 
        SchoolDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task SeedDataAsync()
    {
        await ApplyMigrations();
        await SeedRolesAsync();
        await SeedUsersAsync();
    }

    private async Task ApplyMigrations()
    {
        if (_context.Database.GetPendingMigrations().Any())
        {
            await _context.Database.MigrateAsync();
        }
    }

    private async Task SeedRolesAsync()
    {
        foreach(var appRole in AppRoles.DefaultRoles)
        {
            if(await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == appRole) 
                is not Role role)
            {
                role = new Role(appRole, $"{appRole} Role");

                await _roleManager.CreateAsync(role);
            }

            if(appRole == AppRoles.Admin)
            {
                await AssignPermissionsToRoleAsync(role, AppPermissions.Admin);
            }

            else if(appRole == AppRoles.Basic)
            {
                await AssignPermissionsToRoleAsync(role, AppPermissions.Basic);
            }
        }
    }

    private async Task SeedUsersAsync()
    {
        if (_context.Users.Any()) return;

        User user = new("admin", "admin", Gender.Unspecified, string.Empty, string.Empty, string.Empty,
            "smsadmin.net@gmail.com", "admin");

        user.EmailConfirmed = true;

        var password = new PasswordHasher<User>();

        user.PasswordHash = password.HashPassword(user, "Pass_123$");

        await _userManager.CreateAsync(user);

        await _userManager.AddToRolesAsync(user, AppRoles.DefaultRoles);
    }

    private async Task AssignPermissionsToRoleAsync(Role role, IReadOnlyList<AppPermission> permissions)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);

        foreach(var permission in permissions)
        {
            if(!currentClaims.Any(x => x.Type == AppClaim.Permission && x.Value == permission.Name))
            {
                var userRoleClaim = new UserRoleClaim(role.Id, AppClaim.Permission, permission.Name, permission.Description,
                    permission.Group);

                await _context.RoleClaims.AddAsync(userRoleClaim);

                await _context.SaveChangesAsync();
            }
        }
    }
}
