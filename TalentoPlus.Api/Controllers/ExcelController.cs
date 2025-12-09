using _1_Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TalentoPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExcelController : ControllerBase
{
    private readonly IExcelImportService _importService;
    
    public ExcelController(IExcelImportService importService)
    {
        _importService = importService;
    }
    
    [HttpPost("import")]
    public async Task<IActionResult> ImportEmployees(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Archivo inválido.");

        using var stream = file.OpenReadStream();
        var result = await _importService.ImportEmployeesAsync(stream);

        return Ok(result);
    }

}