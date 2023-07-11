using Microsoft.AspNetCore.Mvc;

namespace Mservices.Options.ActionFilters;

public class CustomResultFilterAttribute : TypeFilterAttribute
{
    public CustomResultFilterAttribute() : base(typeof(CustomResultFilter))
    {
    }
}