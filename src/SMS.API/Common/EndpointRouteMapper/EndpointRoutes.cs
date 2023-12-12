using SMS.Application.Courses.Response;
using SMS.Application.Department.Response;

namespace SMS.API.Common.EndpointRouteMapper;

public static class EndpointRoutes
{
    private static Dictionary<Type, string> _mapRoute = new()
    {
        {typeof(CreateDepartmentResponse), Names.GETDEPARTMENT },
        {typeof(CreateCourseResponse), Names.GETCOURSE }
    };

    public static string GetRouteName(Type type)
    {
        if(_mapRoute.ContainsKey(type)) return _mapRoute[type];

        return string.Empty;
    }

    public static class Names
    {
        public const string GETDEPARTMENT = "GetDepartment";

        public const string GETCOURSE = "GetCourse";
    }
}
