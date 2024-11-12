using AutoMapper;
using DataAccessLayer.BusinessObject;
using Service.DTO;

namespace Service.Mapper;

public class EmployeeMapperProfile: Profile {
    public EmployeeMapperProfile() {
        CreateMap<Employee, EmployeeBaseResponse>();
        CreateMap<EmployeeRequest, Employee>();
    }
}