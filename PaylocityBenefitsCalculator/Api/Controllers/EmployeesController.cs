using Api.Domain.Entities;
using Api.Domain.Paychecks;
using Api.Dtos.Employee;
using Api.Dtos.Paycheck;
using Api.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController(IPaycheckService paycheckService, IEmployeesDao employeesDao, IMapper mapper) : ControllerBase
{
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        Employee? employee = await employeesDao.GetEmployee(id);
        if (employee is not null)
        {
            GetEmployeeDto employeeDto = mapper.Map<GetEmployeeDto>(employee);
            return new ApiResponse<GetEmployeeDto>
            {
                Data = employeeDto,
                Success = true
            };
        }

        return new NotFoundObjectResult(new ApiResponse<GetEmployeeDto>
        {
            Message = $"Employee with id {id} not found",
            Success = false
        });
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        List<Employee> employees = await employeesDao.GetAllEmployees();
        List<GetEmployeeDto> employeeDtos = employees.Select(employee => mapper.Map<GetEmployeeDto>(employee)).ToList();
        return new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employeeDtos,
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get Paychecks for employee and particular year")]
    [HttpGet("{id}/paychecks/{year}")]
    public async Task<ActionResult<ApiResponse<GetPaychecksDto>>> Get(int id, int year)
    {
        Employee? employee = await employeesDao.GetEmployee(id);
        if (employee is null)
        {
            return new NotFoundObjectResult(new ApiResponse<GetEmployeeDto>
            {
                Message = $"Employee with id {id} not found",
                Success = false
            });
        }

        // TODO: would be nice to handle year before birthday and so on

        List<Paycheck> paychecks = [.. paycheckService.GetPaychecks(employee, year)];
        List<PaycheckDto> paycheckDtos = mapper.Map<List<PaycheckDto>>(paychecks);

        return new ApiResponse<GetPaychecksDto>
        {
            Data = new GetPaychecksDto
            {
                Paychecks = paycheckDtos
            },
            Success = true
        };
    }
}
