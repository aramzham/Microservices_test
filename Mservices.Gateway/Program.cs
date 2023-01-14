using Mservices.Gateway.GQL.Queries;
using Mservices.Gateway.Services;
using Mservices.Gateway.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// hot chocolate server
builder.Services.AddGraphQLServer()
    .AddQueryType<Queries>();
// cors
builder.Services.AddCors(options => options.AddPolicy("AllowAllPolicy", builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()));

builder.Services.AddTransient<IBookService, BookService>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
