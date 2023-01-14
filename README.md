# Microservices_test
test project to test some stuff on microservices

- create solution: `dotnet new sln --name MservicesTest`
- create asp.net core project: `dotnet new web --name Mservices.Gateway`
- add project to solution: `dotnet sln add Mservices.Gateway/Mservices.Gateway.csproj`
- build solution: `dotnet build`
- add a nuget package: `dotnet add Mservices.Gateway package HotChocolate.AspNetCore`