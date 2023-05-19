using HashidsNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mservices.GraphDbService.Models;
using Mservices.GraphDbService.Services.Interfaces;

namespace Mservices.GraphDbService.Extensions;

public static class WebApplicationExtensions
{
    public static void MapBookEndpoints(this WebApplication app)
    {
        app.MapPost("/book", Add);
        app.MapGet("/book/{id}", GetById);
        app.MapGet("/book", GetAll);
        app.MapPut("/book/{id:guid}", Update);
        app.MapDelete("/book/{id}", DeleteById);
    }

    private static async Task<IResult> DeleteById(string id, IBookService bookService, IHashids hashids)
    {
        var rawId = hashids.Decode(id);
        if (rawId.Length == 0)
            return Results.NotFound();

        var result = await bookService.DeleteById(rawId[0]);

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
            Results.NotFound,
            failed => Results.BadRequest(failed.Errors));
    }

    internal static async Task<IResult> Add(BookCreateRequest requestModel, IBookService bookService)
    {
        var result = await bookService.Create(requestModel);

        return result.Match(
            Results.Ok,
            failed => Results.BadRequest(failed.Errors));
    }

    [Authorize]
    internal static async Task<IResult> GetById([FromRoute] string id, IBookService bookService, IHashids hashids)
    {
        var rawId = hashids.Decode(id);
        if (rawId.Length == 0)
            return Results.NotFound();

        var result = await bookService.GetById(rawId[0]);

        return result.Match(
            b => Results.Ok(b.MapToResponse(hashids.Encode)),
            Results.NotFound);
    }

    internal static async Task<IResult> GetAll(IBookService bookService)
    {
        var result = await bookService.GetAll();

        return Results.Ok(result);
    }
}