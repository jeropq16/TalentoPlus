using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Api.Models;

namespace TalentoPlus.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            // Lógica para obtener empleados
            return Ok();
        }
    }
}