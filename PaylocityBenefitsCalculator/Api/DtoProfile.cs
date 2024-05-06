using Api.Domain.Entities;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using AutoMapper;

namespace Api;

public class DtoProfile   : Profile
{
    public DtoProfile()
    {
        CreateMap<Employee, GetEmployeeDto>()
            .ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Dependents));
        CreateMap<Dependent, GetDependentDto>();
        //CreateMap<Paycheck, GetPaycheckDto>()
        //    .ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Employee.Dependents));
    }
}

