using Microsoft.AspNetCore.Mvc;
using rizer.Services;

namespace rizer.Controller;

[ApiController]
[Route("api/[controller]")]
public class CountController : ControllerBase
{
    private readonly ICounterService _counterService;

    public CountController(ICounterService counterService)
    {
        _counterService = counterService;
    }

    [HttpGet]
    [Route("count")]
    public int GetCount()
    {
        return _counterService.GetCount();
    }

    [HttpGet("stop")]
    public void StopCount()
    {
        _counterService.Stop();
    }
}