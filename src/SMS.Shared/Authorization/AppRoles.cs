using System.Collections.ObjectModel;

namespace SMS.Shared.Authorization;
public static class AppRoles
{
    public const string Admin = nameof(Admin);
    public const string User = nameof(User);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        User,
    });

    public static bool IsDefault(string roleName) => 
        DefaultRoles.Contains(roleName);
}
