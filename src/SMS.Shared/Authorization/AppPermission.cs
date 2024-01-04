using System.Collections.ObjectModel;

namespace SMS.Shared.Authorization;
public record AppPermission(string Feature, string Action, string Group, string Description, bool IsBasic = false)
{
    public string Name { get; set; } = string.Empty;

    public static string Builder(string feature, string action) =>
           $"Permision.{feature}.{action}";
}

public class AppPermissions
{
    private static readonly AppPermission[] _all = new AppPermission[]
    {
        new(AppFeatures.Student, AppActions.Create, AppRoleGroup.Management, "Create Student"),
        new(AppFeatures.Student, AppActions.Update, AppRoleGroup.Management, "Update Student"),
        new(AppFeatures.Student, AppActions.Delete, AppRoleGroup.Management, "Delete Student"),
        new(AppFeatures.Student, AppActions.Read, AppRoleGroup.Management, "Read Student", true),

        new(AppFeatures.User, AppActions.Create, AppRoleGroup.SystemAccess, "Create User"),
        new(AppFeatures.User, AppActions.Read, AppRoleGroup.SystemAccess, "Read User"),
        new(AppFeatures.User, AppActions.Update, AppRoleGroup.SystemAccess, "Update User"),
        new(AppFeatures.User, AppActions.Delete, AppRoleGroup.SystemAccess, "Delete User"),

        new(AppFeatures.Role, AppActions.Create, AppRoleGroup.SystemAccess, "Create Role"),
        new(AppFeatures.Role, AppActions.Read, AppRoleGroup.SystemAccess, "Read Role"),
        new(AppFeatures.Role, AppActions.Update, AppRoleGroup.SystemAccess, "Update Role"),
        new(AppFeatures.Role, AppActions.Delete, AppRoleGroup.SystemAccess, "Delete Role"),

    };

    public static IReadOnlyList<AppPermission> Admin { get; } =
        new ReadOnlyCollection<AppPermission>(_all.Where(p => !p.IsBasic).ToArray());

    public static IReadOnlyList<AppPermission> Basic { get; } =
        new ReadOnlyCollection<AppPermission>(_all.Where(p => p.IsBasic).ToArray());
}
