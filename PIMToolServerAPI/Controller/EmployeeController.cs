using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Response;
using Service.DTO;
using Service.Service;

namespace PIMToolServerAPI.Controller;

[ApiController, Route("/api/v1/employees"), Authorize]
public class EmployeeController(EmployeeService employeeService): ControllerBase {
    private readonly EmployeeService _employeeService = employeeService;

    [HttpGet("search")]
    public async Task<IActionResult> GetEmployees([FromQuery] int page = 1, [FromQuery] int size = 10, [FromQuery] string keyword = "all")
    {
        var employees = await _employeeService.SearchEmployeesAsync(keyword, page, size);
        return Ok(employees);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseEntity<EmployeeBaseResponse>>> GetEmployeeById([FromRoute] int id)
    {
        var response = await _employeeService.FindById(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseEntity<EmployeeBaseResponse>>> InsertEmployee([FromBody] EmployeeRequest employeeRequest)
    {
        var response = await _employeeService.CreateEmployeeAsync(employeeRequest);
        return CreatedAtAction(nameof(GetEmployeeById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseEntity<GroupResponseDto>>> UpdateEmployee(int id, [FromBody] EmployeeRequest employeeRequest)
    {
        var response = await _employeeService.UpdateGroupAsync(id, employeeRequest);
        if (response.StatusCode == 404)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseEntity<Object>>> RemoveEmployee([FromRoute] int id)
    {
        var result = await _employeeService.DeleteEmployeeAsync(id);
        return StatusCode(result.StatusCode, result);
    }
}