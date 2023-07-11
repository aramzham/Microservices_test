using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mservices.Options.ActionFilters;

public class TimingActionFilter : IActionFilter
{
    private readonly Stopwatch _stopwatch;
    private readonly ILogger<TimingActionFilter> _logger;

    public TimingActionFilter(ILogger<TimingActionFilter> logger)
    {
        _logger = logger;
        _stopwatch = new Stopwatch();
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch.Start();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;
        // Log or use the elapsed time as needed
        _logger.LogInformation($"action lasted {elapsedMilliseconds}ms");
    }
}