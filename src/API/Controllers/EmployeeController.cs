using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.UpdateEmployee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Employees.Commands.DeleteEmployee;
using Application.Employees.Queries.GetAllEmployees;
using Application.Employees.Queries.GetEmployeeById;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery(id));
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            var employeeId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeId }, employeeId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
            if (id != command.Id) return BadRequest();

            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));
            return result ? NoContent() : NotFound();
        }
    }
}
