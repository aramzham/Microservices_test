using Mservices.GraphDbService.Data;
using Mservices.GraphDbService.Models;

namespace Mservices.GraphDbService.Extensions;

public static class DomainModelExtensions
{
    public static BookResponse MapToResponse(this Book book, Func<int, string> convertToHashId)
    {
        return new BookResponse(convertToHashId(book.Id), book.Title, book.AuthorNames, book.Year);
    }
}