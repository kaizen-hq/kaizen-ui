using Microsoft.AspNetCore.Mvc;
using Kaizen.Sample.Web.Services;

namespace Kaizen.Sample.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComponentSourceController : ControllerBase
{
    private readonly ComponentSourceService _sourceService;

    public ComponentSourceController(ComponentSourceService sourceService)
    {
        _sourceService = sourceService;
    }

    [HttpGet("{*path}")]
    public async Task<IActionResult> GetSource(string path)
    {
        var source = await _sourceService.GetComponentSourceAsync(path);
        return Ok(source);
    }
}
