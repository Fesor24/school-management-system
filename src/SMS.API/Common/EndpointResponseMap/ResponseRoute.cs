using SMS.Application.Department.Response;

namespace SMS.API.Common.EndpointResponseMap;

public static class ResponseRoute
{
    private static Dictionary<Type, string> _mapRoute = new Dictionary<Type, string>
    {
        {typeof(CreateDepartmentResponse), GETDEPARTMENT }
    };

    public static string GetRouteName(Type type)
    {
        if(_mapRoute.ContainsKey(type)) return _mapRoute[type];

        return string.Empty;
    }

    public const string GETDEPARTMENT = "GetDepartment";

    public const string CREATECOURSE = "CreateCourse";
}
