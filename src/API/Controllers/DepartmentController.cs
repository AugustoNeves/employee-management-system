using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Departments.Queries.GetAllDepartments;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _mediator.Send(new GetAllDepartmentsQuery());
            return Ok(departments);
        }

    }
}
