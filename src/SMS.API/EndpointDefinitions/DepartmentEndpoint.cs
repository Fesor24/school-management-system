using MediatR;
using SMS.API.Abstractions;
using SMS.API.Common.EndpointRouteMapper;
using SMS.API.Extensions;
using SMS.Application.Department.Commands.CreateDepartment;
using SMS.Application.Department.Commands.DeleteDepartment;
using SMS.Application.Department.Commands.UpdateDepartment;
using SMS.Application.Department.Queries.GetDepartmentById;
using SMS.Application.Department.Queries.GetDepartments;
using SMS.Application.Department.Response;

namespace SMS.API.EndpointDefinitions;

public class DepartmentEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        const string ENDPOINT = "Department";

        app.MediatorGet<GetDepartmentsRequest, IReadOnlyList<GetDepartmentResponse>>(ENDPOINT, "/");

        app.MediatorGet<GetDepartmentByIdRequest, GetDepartmentResponse>(ENDPOINT, "/{id}", 
            EndpointRoutes.Names.GETDEPARTMENT);

        app.MediatorPost<CreateDepartmentCommand, CreateDepartmentResponse>(ENDPOINT, "/");

        app.MediatorDelete<DeleteDepartmentCommand, Unit>(ENDPOINT, "/{id}");

        app.MediatorPut<UpdateDepartmentCommand, Unit>(ENDPOINT, "/");
    }
}
