using Microsoft.AspNetCore.Mvc;
using Mservices.Options.ActionFilters;

namespace Mservices.Options.Controllers;

// Applying controller filter to a controller
// [TypeFilter(typeof(CustomAuthorizationFilter))]
[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    public IResult Login()
    {
        return Results.Ok("logged in");
    }

    // Authorization Filter
    // Authorization filters are used to check if a user is authorized to access a particular resource or page. They are executed before the action method is invoked.
    [TypeFilter(typeof(CustomAuthorizationFilter))]
    public IResult UserMustBeKnown()
    {
        return Results.Ok("i am a known user");
    }

    // Resource filter
    // Resource filters are used to perform tasks that are related to a particular resource. They are executed before and after the action method.
    [TypeFilter(typeof(LoggingResourceFilter))]
    public IResult LogResource()
    {
        _logger.LogInformation("executing action body");
        return Results.Ok("method has executed");
    }

    // Action filters
    // Action filters are used to perform tasks that are related to a particular action method. They are executed before and after the action method.
    [TimingActionFilter]
    public IResult LogAction()
    {
        _logger.LogInformation("executing action body");
        return Results.Ok("method has executed");
    }

    // Exception filters
    // Exception filters are used to handle exceptions that occur during the execution of an action method. They are executed when an exception is thrown.
    [TypeFilter(typeof(ExceptionHandlingFilter))]
    public IResult LogException()
    {
        throw new Exception("exception thrown");
    }

    // Result filters
    // Result filters are used to perform actions before and after the execution of an action result. They can modify the result, add headers, or perform additional operations.
    [CustomResultFilter]
    public IResult LogResult()
    {
        return Results.Ok("check out headers");
    }
}