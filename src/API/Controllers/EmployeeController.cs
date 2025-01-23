using Microsoft.AspNetCore.Mvc;
using Employee.Application.Users.Models;
using Employee.Application.Services;
using Employee.Domain.SeedWork;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService, INotification notification) : base(notification)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeRequest employeeRequest)
        {
            var result = await _employeeService.AddAsync(employeeRequest);
            return Response(result);
        }
    }
}