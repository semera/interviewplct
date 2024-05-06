using Api.Domain.Entities;
using Api.Dtos.Dependent;
using Api.Models;
using Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController(IEmployeesDao employeesDao, IMapper mapper) : ControllerBase // Added IMapper dependency
{
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        Dependent? dependent = await employeesDao.GetDependent(id);
        if (dependent is not null)
        {
            GetDependentDto employeeDto = mapper.Map<GetDependentDto>(dependent);
            return new ApiResponse<GetDependentDto>
            {
                Data = employeeDto,
                Success = true
            };
        }

        return new NotFoundObjectResult(new ApiResponse<GetDependentDto>
        {
            Message = $"Dependent with id {id} not found",
            Success = false

        });
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        List<Dependent> dependes = await employeesDao.GetAllDependent();
        List<GetDependentDto> dependentDtos = dependes.Select(dependent => mapper.Map<GetDependentDto>(dependent)).ToList();
        return new ApiResponse<List<GetDependentDto>>
        {
            Data = dependentDtos,
            Success = true
        };
    }
}
