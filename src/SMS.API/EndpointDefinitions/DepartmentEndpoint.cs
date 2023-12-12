using SMS.API.Abstractions;
using SMS.API.Common.EndpointResponseMap;
using SMS.API.Extensions;
using SMS.Application.Department.Commands.CreateDepartment;
using SMS.Application.Department.Queries.GetDepartmentById;
using SMS.Application.Department.Queries.GetDepartments;
using SMS.Application.Department.Response;
using SMS.Domain.Shared;

namespace SMS.API.EndpointDefinitions;

public class DepartmentEndpoints : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        const string ENDPOINT = "Departments";

        app.MediatorGet<GetDepartmentsRequest, Result<IReadOnlyList<GetDepartmentResponse>, Error>, 
            IReadOnlyList<GetDepartmentResponse>, Error>(ENDPOINT, "/");

        //app.MediatorGet<GetDepartmentByIdRequest, Result<GetDepartmentResponse, Error>, GetDepartmentResponse, Error>(ENDPOINT,
        //    "/", ResponseRoute.GETDEPARTMENT);

        app.MediatorPost<CreateDepartmentCommand, Result<CreateDepartmentResponse, Error>, CreateDepartmentResponse, Error>(
            ENDPOINT, "/");
    }
}
