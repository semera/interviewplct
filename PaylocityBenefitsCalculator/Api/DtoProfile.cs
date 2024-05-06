using System.Reflection;
using Api.Domain.Entities;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using AutoMapper;
using Api.Dtos.Paycheck;

namespace Api;

public class DtoProfile   : Profile
{
    public DtoProfile()
    {
        CreateMap<Employee, GetEmployeeDto>();
          //  .ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Dependents));
        CreateMap<Dependent, GetDependentDto>();
        CreateMap<Paycheck, PaycheckDto>();
      //  CreateMap<List<Paycheck>, List<PaycheckDto>>();

        CreateMap<PaycheckItem, PaycheckItemDto>();
        //CreateMap<Paycheck, Pay>();
        //CreateMap<Paycheck, GetPaycheckDto>();
        //    .ForMember(dest => dest.Dependents, opt => opt.MapFrom(src => src.Employee.Dependents));
    }
}

