using AutoMapper;
using SMS.Application.Courses.Request;
using SMS.Application.Courses.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;
using SMS.Domain.Models.Course;

namespace SMS.Application.Common.Mappings;
internal class CourseMappings : Profile
{
    public CourseMappings()
    {
        CreateMap<Course, GetCourseResponse>()
            .ConstructUsing(x => new(x.Id, x.CourseInfo.Name, x.CourseInfo.Code, x.Unit));

        CreateMap<Course, CreateCourseResponse>()
            .ConstructUsing(x => new CreateCourseResponse(x.Id, x.CourseInfo.Name, x.CourseInfo.Code, x.Unit));

        CreateMap<CreateCourseRequest, CourseModel>();
    }
}
