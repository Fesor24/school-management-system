using AutoMapper;
using SMS.Application.Department.Response;
using DepartmentEntity =  SMS.Domain.Aggregates.DepartmentAggregates.Department;

namespace SMS.Application.Common.Mappings;
public class DepartmentMappings : Profile
{
    public DepartmentMappings()
    {
        CreateMap<DepartmentEntity, CreateDepartmentResponse>();
        CreateMap<DepartmentEntity, GetDepartmentResponse>();
    }
}
