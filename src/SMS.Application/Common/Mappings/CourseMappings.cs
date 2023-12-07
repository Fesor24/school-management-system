using AutoMapper;
using SMS.Application.Courses.Response;
using SMS.Domain.Entities;

namespace SMS.Application.Common.Mappings;
internal class CourseMappings : Profile
{
    public CourseMappings()
    {
        CreateMap<Course, GetCourseResponse>();
    }
}
