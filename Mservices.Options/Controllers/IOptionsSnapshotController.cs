using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mservices.Options.Controllers;

[ApiController]
[Route("[controller]")]
public class IOptionsSnapshotController : ControllerBase
{
    private readonly MyConfig _config;
    private int _updates = -1;
    
    public IOptionsSnapshotController(IOptionsSnapshot<MyConfig> config)
    {
        _config = config.Value;
        _updates++;
    }  
    
    public IResult Get()
    {
        // IOptions<T> is a Singleton
        return Results.Ok(new
        {
            MyName = _config.Name,
            Updates = _updates
        });
    }
}