using FluentValidation;
using Mapster;
using Mservices.GraphDbService.Extensions;
using Mservices.GraphDbService.Repositories;
using Mservices.GraphDbService.Repositories.Interfaces;
using Mservices.GraphDbService.Services;
using Mservices.GraphDbService.Services.Interfaces;
using Mservices.GraphDbService.Validation;
using Neo4j.Driver;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});

// validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
// repos
builder.Services.AddTransient<IBookRepository, BookRepository>();
// services
builder.Services.AddTransient<IBookService, BookService>();
// mapper
builder.Services.AddMapster();
// db
builder.Services.AddSingleton<IDriver>(_ =>
{
    var databaseSection = builder.Configuration.GetSection("Database");
    return GraphDatabase.Driver(databaseSection.GetValue<string>("Server"), AuthTokens.Basic(databaseSection.GetValue<string>("UserName"), databaseSection.GetValue<string>("Password")));
});

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapBookEndpoints();

app.Run();