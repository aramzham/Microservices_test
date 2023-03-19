using MapsterMapper;
using Mservices.GraphDbService.Data;
using Mservices.GraphDbService.Models;
using Mservices.GraphDbService.Repositories.Interfaces;
using Mservices.GraphDbService.Services.Interfaces;
using Mservices.GraphDbService.Validation;
using OneOf;
using OneOf.Types;

namespace Mservices.GraphDbService.Services;

public class BookService : IBookService
{
    private readonly BookRequestValidator _bookRequestValidator;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    
    public BookService(BookRequestValidator bookRequestValidator, IBookRepository bookRepository, IMapper mapper)
    {
        _bookRequestValidator = bookRequestValidator;
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<OneOf<Success<Book>, ValidationFailed>> Create(BookCreateRequest book)
    {
        var validationResult = await _bookRequestValidator.ValidateAsync(book);
        if (!validationResult.IsValid)
            return new ValidationFailed(validationResult.Errors);

        var createRequest = _mapper.Map<Book>(book);
        var result = await _bookRepository.Add(createRequest);
        return new Success<Book>(result);
    }

    public Task<OneOf<Success<Book>, NotFound>> GetById(Guid id)
    {
        return _bookRepository.GetById(id);
    }

    public Task<List<Book>> GetAll()
    {
        return _bookRepository.GetAll();
    }

    public async Task<OneOf<Book, NotFound, ValidationFailed>> Update(BookUpdateRequest book)
    {
        var validationResult = await _bookRequestValidator.ValidateAsync(book);
        if (!validationResult.IsValid)
            return new ValidationFailed(validationResult.Errors);

        var bookInDbResponse = await _bookRepository.GetById(book.Id);
        if (bookInDbResponse.IsT1)
            return new NotFound();

        var updateRequest = _mapper.Map<Book>(book);
        var updatedBook = await _bookRepository.Update(updateRequest);
        return updatedBook;
    }

    public Task<OneOf<Success, NotFound>> DeleteById(Guid id)
    {
        return _bookRepository.DeleteById(id);
    }
}