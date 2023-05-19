using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/token", ([FromBody] TokenGenerationRequest request, IConfiguration config) =>
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(config["TokenSecret"]!);

    var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Sub, request.Email),
        new(JwtRegisteredClaimNames.Email, request.Email),
        new("userid", request.UserId.ToString())
    };

    foreach (var claimPair in request.CustomClaims)
    {
        var jsonElement = (JsonElement)claimPair.Value;
        var valueType = jsonElement.ValueKind switch
        {
            JsonValueKind.True => ClaimValueTypes.Boolean,
            JsonValueKind.False => ClaimValueTypes.Boolean,
            JsonValueKind.Number => ClaimValueTypes.Double,
            _ => ClaimValueTypes.String
        };

        var claim = new Claim(claimPair.Key, claimPair.Value.ToString(), valueType);
        
        claims.Add(claim);
    }

    var tokenDescriptor = new SecurityTokenDescriptor()
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.Add(TokenLifetime),
        Issuer = "https://my.idserver.com",
        Audience = "https://my.website.com",
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Sha256)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwt = tokenHandler.WriteToken(token);
    return Results.Ok(jwt);
});

app.Run();