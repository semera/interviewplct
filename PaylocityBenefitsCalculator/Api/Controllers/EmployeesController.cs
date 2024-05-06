using Api.Domain.Entities;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController(IEmployeesDao employeesDao, IMapper mapper) : ControllerBase
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
}
