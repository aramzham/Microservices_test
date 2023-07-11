using Microsoft.AspNetCore.Mvc.Filters;

namespace Mservices.Options.ActionFilters;

public class CustomResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        // Modify the result or add headers before the execution
        context.HttpContext.Response.Headers.TryAdd("CustomHeader", "CustomValue");
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        // Perform additional actions after the execution
    }
}