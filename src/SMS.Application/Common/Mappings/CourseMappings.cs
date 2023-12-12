using AutoMapper;
using SMS.Application.Common.Response;
using SMS.Application.Courses.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;

namespace SMS.Application.Common.Mappings;
internal class CourseMappings : Profile
{
    public CourseMappings()
    {
        CreateMap<Course, GetCourseResponse>();
        //CreateMap<Course, CreateCourseResponse>()
        //    .ForMember(x => x.Code, o => o.MapFrom(s => s.CourseInfo.Code))
        //    .ForMember(x => x.Name, o => o.MapFrom(s => s.CourseInfo.Name));
        CreateMap<Course, CreateCourseResponse>()
            .ConstructUsing(x => new CreateCourseResponse(x.Id, x.CourseInfo.Name, x.CourseInfo.Code, x.Unit));
    }
}
