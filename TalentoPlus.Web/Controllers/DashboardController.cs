using Microsoft.AspNetCore.Mvc;
using TalentoPlus.Api.Models;

namespace TalentoPlus.Api.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDashboard()
        {
            return Ok();
        }
    }
}