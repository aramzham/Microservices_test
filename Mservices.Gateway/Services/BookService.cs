using Mservices.Gateway.Models;
using Mservices.Gateway.Services.Interfaces;

namespace Mservices.Gateway.Services;

public class BookService : IBookService
{
    public Task<List<Book>> GetAll()
    {
        // call repository
        return Task.FromResult(new List<Book>()
        {
            new Book()
            {
                Id = Guid.NewGuid(),
                Title = "Verq Hayastani",
                Author = new Author()
                {
                    Id = Guid.NewGuid(),
                    Name = "Khachatur Abovyan"
                }
            }
        });
    }
}