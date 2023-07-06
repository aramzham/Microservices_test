using Mservices.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<MyConfig>(builder.Configuration.GetSection("MyConfig"));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();