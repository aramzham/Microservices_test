using Mservices.GraphDbService.Data;
using Mservices.GraphDbService.Models;
using Mservices.GraphDbService.Validation;
using OneOf;
using OneOf.Types;

namespace Mservices.GraphDbService.Services.Interfaces;

public interface IBookService
{
    Task<OneOf<Success<Book>, ValidationFailed>> Create(BookCreateRequest book);
    Task<OneOf<Success<Book>, NotFound>> GetById(Guid id);
    Task<List<Book>> GetAll();
    Task<OneOf<Book, NotFound, ValidationFailed>> Update(BookUpdateRequest book);
    Task<OneOf<Success, NotFound>> DeleteById(Guid id);
}