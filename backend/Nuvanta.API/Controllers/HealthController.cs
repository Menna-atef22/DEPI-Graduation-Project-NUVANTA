using Microsoft.AspNetCore.Mvc;
using Nuvanta.Application.Interfaces;

namespace Nuvanta.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IDatabaseDiagnosticsService _diagnostics;
    public HealthController(IDatabaseDiagnosticsService diagnostics) => _diagnostics = diagnostics;

    /// <summary>API is up.</summary>
    [HttpGet]
    public IActionResult Get() => Ok(new { status = "Healthy", service = "Nuvanta.API", utc = DateTime.UtcNow });

    /// <summary>Connects to SQL Server and reads all 41 tables through EF Core.</summary>
    [HttpGet("database")]
    public async Task<IActionResult> Database(CancellationToken ct)
    {
        var result = await _diagnostics.CheckAsync(ct);
        return result.CanConnect && result.TablesFailed == 0 ? Ok(result) : StatusCode(503, result);
    }
}
