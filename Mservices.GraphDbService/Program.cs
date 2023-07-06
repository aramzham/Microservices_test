using System.Text;
using FluentValidation;
using HashidsNet;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Mservices.GraphDbService.Extensions;
using Mservices.GraphDbService.Identity;
using Mservices.GraphDbService.Repositories;
using Mservices.GraphDbService.Repositories.Interfaces;
using Mservices.GraphDbService.Services;
using Mservices.GraphDbService.Services.Interfaces;
using Mservices.GraphDbService.Swagger;
using Mservices.GraphDbService.Validation;
using Neo4j.Driver;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});
// jwt
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, policy => policy.RequireClaim(IdentityData.AdminUserClaimName, "true"));
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
// hashids
builder.Services.AddSingleton<IHashids>(_ => new Hashids("my_salt_keep_it_somewhere_secure", 11)); // 11 like in YouTube
// swagger
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapBookEndpoints();

app.Run();
