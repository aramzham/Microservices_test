using Mservices.GraphDbService.Data;
using OneOf;
using OneOf.Types;

namespace Mservices.GraphDbService.Repositories.Interfaces;

public interface IBookRepository : IBaseRepository
{
    Task<Book> Add(Book book);
    Task<OneOf<Success<Book>, NotFound>> GetById(Guid id);
    Task<Book> Update(Book book);
    Task<List<Book>> GetAll();
    Task<OneOf<Success,NotFound>> DeleteById(Guid id);
}