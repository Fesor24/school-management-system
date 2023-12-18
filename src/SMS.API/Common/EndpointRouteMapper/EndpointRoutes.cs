using SMS.Application.Courses.Response;
using SMS.Application.Department.Response;

namespace SMS.API.Common.EndpointRouteMapper;

internal static class EndpointRoutes
{
    private static Dictionary<Type, string> _mapRoute = new()
    {
        {typeof(CreateDepartmentResponse), Names.Department.GETDEPARTMENT },
        {typeof(CreateCourseResponse), Names.Course.GETCOURSE }
    };

    internal static string GetRouteName(Type type)
    {
        if(_mapRoute.ContainsKey(type)) return _mapRoute[type];

        return string.Empty;
    }

    internal static class Names
    {
        internal static class Course
        {
            internal const string GETCOURSE = "GetCourse";
            internal const string GETCOURSES = "GetCourses";
        }

        internal static class Department
        {
            internal const string GETDEPARTMENT = "GetDepartment";
            internal const string GETDEPARTMENTS = "GetDepartments";
        }
        
    }
}
