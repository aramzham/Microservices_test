using Microsoft.AspNetCore.Mvc;
using Mservices.GraphDbService.Models;
using Mservices.GraphDbService.Services.Interfaces;

namespace Mservices.GraphDbService.Extensions;

public static class WebApplicationExtensions
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapPost("/book", Add);
        app.MapGet("/book/{id:guid}", GetById);
        app.MapGet("/book", GetAll);
        app.MapPut("/book/{id:guid}", Update);
        app.MapDelete("/book/{id:guid}", DeleteById);
    }

    private static async Task<IResult> DeleteById(Guid id, IBookService bookService)
    {
        var result = await bookService.DeleteById(id);

        return result.Match(
            Results.Ok, 
            Results.NotFound);
    }

    internal static async Task<IResult> Update(Guid id, [FromBody] BookUpdateRequest updateRequest,
        [FromServices] IBookService bookService)
    {
        var result = await bookService.Update(updateRequest);

        return result.Match(
            Results.Ok,
            _ => Results.NotFound(),
            failed => Results.BadRequest(failed.Errors));
    }

    internal static async Task<IResult> Add(BookCreateRequest requestModel, IBookService bookService)
    {
        var result = await bookService.Create(requestModel);

        return result.Match(
            Results.Ok,
            failed => Results.BadRequest(failed.Errors));
    }

    internal static async Task<IResult> GetById(Guid id, IBookService bookService)
    {
        var result = await bookService.GetById(id);

        return result.Match(
            Results.Ok,
            _ => Results.NotFound());
    }

    internal static async Task<IResult> GetAll(IBookService bookService)
    {
        var result = await bookService.GetAll();

        return Results.Ok(result);
    }
}