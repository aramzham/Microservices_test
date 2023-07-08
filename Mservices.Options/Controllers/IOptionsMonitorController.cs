using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mservices.Options.Controllers;

[ApiController]
[Route("[controller]")]
public class IOptionsMonitorController : ControllerBase
{
    private static int _updates;
    
    private readonly IOptionsMonitor<MyConfig> _config;
    private readonly ILogger<IOptionsMonitorController> _logger;
    
    public IOptionsMonitorController(IOptionsMonitor<MyConfig> config, ILogger<IOptionsMonitorController> logger)
    {
        _config = config;
        _logger = logger;
        _config.OnChange((a, b) =>
        {
            _logger.LogInformation("a.Name is {aName}", a.Name);
            _logger.LogInformation("b.Length is {bLength}", b.Length);
            _updates++;
        });
    }

    public IResult Get()
    {
        return Results.Ok(new
        {
            MyName = _config.CurrentValue.Name,
            Updates = _updates
        });
    }
}