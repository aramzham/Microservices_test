using Mservices.Options;
using Mservices.Options.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(TimingActionFilter));
    options.Filters.Add(typeof(CustomResultFilter)); // Registering global filter
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<MyConfig>(builder.Configuration.GetSection("MyConfig"));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();