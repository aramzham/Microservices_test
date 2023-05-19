using Mservices.GraphDbService.Data;
using OneOf;
using OneOf.Types;

namespace Mservices.GraphDbService.Repositories.Interfaces;

public interface IBookRepository : IBaseRepository
{
    Task<Book> Add(Book book);
    Task<Book?> GetById(int id);
    Task<Book> Update(Book book);
    Task<List<Book>> GetAll();
    Task<bool> DeleteById(int id);
}