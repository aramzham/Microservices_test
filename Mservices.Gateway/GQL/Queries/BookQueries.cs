using Mservices.Gateway.Models;
using Mservices.Gateway.Services.Interfaces;

namespace Mservices.Gateway.GQL.Queries;

public partial class Queries
{
    private readonly ILogger<Queries> _logger;

    public Queries(ILogger<Queries> logger)
    {
        _logger = logger;
    }
    
    public async Task<List<Book>> GetBooks([Service] IBookService bookService)
    {
        var correlationId = Guid.NewGuid(); // fake
        var books = await bookService.GetAll();
        _logger.LogInformation("[{correlationId}] [BookQueries.Books] is called", correlationId);

        return books;
    }
}