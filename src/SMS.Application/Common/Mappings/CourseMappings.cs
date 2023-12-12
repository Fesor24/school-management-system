using AutoMapper;
using SMS.Application.Courses.Response;
using SMS.Domain.Aggregates.DepartmentAggregates;

namespace SMS.Application.Common.Mappings;
internal class CourseMappings : Profile
{
    public CourseMappings()
    {
        CreateMap<Course, GetCourseResponse>();
        CreateMap<Course, CreateCourseResponse>();
    }
}
