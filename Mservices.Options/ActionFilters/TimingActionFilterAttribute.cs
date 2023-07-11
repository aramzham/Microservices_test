using Microsoft.AspNetCore.Mvc;

namespace Mservices.Options.ActionFilters;

public class TimingActionFilterAttribute : TypeFilterAttribute
{
    public TimingActionFilterAttribute() : base(typeof(TimingActionFilter))
    {
    }
}