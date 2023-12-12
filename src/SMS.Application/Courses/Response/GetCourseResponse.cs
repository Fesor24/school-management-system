using SMS.Application.Common.Response;

namespace SMS.Application.Courses.Response;
public record GetCourseResponse(Guid Id, string Name, string Code, int Unit) : CreateResponse(Id);
