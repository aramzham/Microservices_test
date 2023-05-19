using Mservices.GraphDbService.Data;
using Mservices.GraphDbService.Models;
using Mservices.GraphDbService.Validation;
using OneOf;
using OneOf.Types;

namespace Mservices.GraphDbService.Services.Interfaces;

public interface IBookService
{
    Task<OneOf<Book, ValidationFailed>> Create(BookCreateRequest createRequest);
    Task<OneOf<Book, NotFound>> GetById(int id);
    Task<List<Book>> GetAll();
    Task<OneOf<Book, NotFound, ValidationFailed>> Update(BookUpdateRequest book);
    Task<OneOf<Success, NotFound>> DeleteById(int id);
}