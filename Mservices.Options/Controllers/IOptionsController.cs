using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Mservices.Options.Controllers;

[ApiController]
[Route("[controller]")]
public class IOptionsController : ControllerBase
{
    private readonly IOptions<MyConfig> _config;
    private int _updates = 0;

    public IOptionsController(IOptions<MyConfig> config)
    {
        _config = config;
    }

    public IResult Get()
    {
        // IOptions<T> is a Singleton
        return Results.Ok(new
        {
            MyName = _config.Value.Name,
            Updates = _updates
        });
    }
}