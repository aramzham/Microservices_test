using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mservices.Options.ActionFilters;

public class ExceptionHandlingFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionHandlingFilter> _logger;

    public ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        // Handle the exception or perform any necessary actions
        var exception = context.Exception;
        // Add your logic to handle the exception, such as logging or returning a specific response
        _logger.LogError(exception, "Exception occurred");
        // Set the result to indicate that the exception has been handled
        context.Result = new ContentResult
        {
            Content = "An error occurred.",
            StatusCode = 500
        };
    }
}