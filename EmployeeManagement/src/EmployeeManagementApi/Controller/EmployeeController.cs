using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Domain.Dtos.Request;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagementApi.Controller
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthService _authService;

        public EmployeeController(IEmployeeService employeeService,
            IAuthService authService)
        {
            _employeeService = employeeService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest request)
        {
            var validate = request.Validate();
            if (!validate.IsValid)
                return BadRequest(validate.Errors);

            Employee currentUser = await _authService.GetCurrentUser();
            if (currentUser == null)
            {
                return Unauthorized("User not authenticated.");
            }

            Employee employee = await _employeeService.AddEmployee(request, currentUser);
            return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee is null) return NotFound();
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeRequest request)
        {
            var validate = request.Validate();
            if (!validate.IsValid && (request.Id.HasValue))
                return BadRequest(validate.Errors);

            var updatedEmployee = await _employeeService.UpdateEmployee(request);
            if (updatedEmployee is null)
                return NotFound();

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployee(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
