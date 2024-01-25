using System.Collections.ObjectModel;

namespace SMS.Shared.Authorization;
public record AppPermission(string Feature, string Action, string Group, string Description, bool IsBasic = false)
{
    public string Name => Builder(Feature, Action);

    public static string Builder(string feature, string action) =>
           $"Permision.{feature}.{action}";
}

public class AppPermissions
{
    private static readonly AppPermission[] _all = new AppPermission[]
    {
        new(AppFeature.Student, AppAction.Create, AppRoleGroup.Management, "Create Student"),
        new(AppFeature.Student, AppAction.Update, AppRoleGroup.Management, "Update Student"),
        new(AppFeature.Student, AppAction.Delete, AppRoleGroup.Management, "Delete Student"),
        new(AppFeature.Student, AppAction.Read, AppRoleGroup.Management, "Read Student", true),

        new(AppFeature.User, AppAction.Create, AppRoleGroup.SystemAccess, "Create User"),
        new(AppFeature.User, AppAction.Read, AppRoleGroup.SystemAccess, "Read User"),
        new(AppFeature.User, AppAction.Update, AppRoleGroup.SystemAccess, "Update User"),
        new(AppFeature.User, AppAction.Delete, AppRoleGroup.SystemAccess, "Delete User"),

        new(AppFeature.Role, AppAction.Create, AppRoleGroup.SystemAccess, "Create Role"),
        new(AppFeature.Role, AppAction.Read, AppRoleGroup.SystemAccess, "Read Role"),
        new(AppFeature.Role, AppAction.Update, AppRoleGroup.SystemAccess, "Update Role"),
        new(AppFeature.Role, AppAction.Delete, AppRoleGroup.SystemAccess, "Delete Role"),

    };

    public static IReadOnlyList<AppPermission> Admin { get; } =
        new ReadOnlyCollection<AppPermission>(_all.Where(p => !p.IsBasic).ToArray());

    public static IReadOnlyList<AppPermission> Basic { get; } =
        new ReadOnlyCollection<AppPermission>(_all.Where(p => p.IsBasic).ToArray());
}
