﻿using MediatR;
using SMS.API.Abstractions;
using SMS.API.Common.EndpointRouteMapper;
using SMS.API.Extensions;
using SMS.Application.Courses.Commands.CreateCourse;
using SMS.Application.Courses.Commands.UpdateCourse;
using SMS.Application.Courses.Queries.GetCourseById;
using SMS.Application.Courses.Queries.GetDepartmentCourses;
using SMS.Application.Courses.Response;

namespace SMS.API.EndpointDefinitions;
public class CourseEndpoint : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        const string ENDPOINT = "Course";

        app.MediatorPost<CreateCourseCommand, CreateCourseResponse>(ENDPOINT, "/");

        app.MediatorGet<GetCourseByIdRequest, GetCourseResponse>(ENDPOINT, "/{id}", 
            EndpointRoutes.Names.Course.GETCOURSE);

        app.MediatorPut<UpdateCourseCommand, Unit>(ENDPOINT, "/");

        app.MediatorGet<GetDepartmentCoursesRequest, List<GetCourseResponse>>(ENDPOINT, 
            "/department/{departmentId}", EndpointRoutes.Names.Course.GETCOURSES);
    }
}
