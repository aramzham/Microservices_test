using Microsoft.AspNetCore.Mvc.Filters;

namespace Mservices.Options.ActionFilters;

public class LoggingResourceFilter : IResourceFilter
{
    private readonly ILogger<LoggingResourceFilter> _logger;

    public LoggingResourceFilter(ILogger<LoggingResourceFilter> logger)
    {
        _logger = logger;
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        _logger.LogInformation($"Executing resource filter - Before action method");
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        _logger.LogInformation("Executing resource filter - After action method");
    }
}