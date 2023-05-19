using HashidsNet;
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
    private readonly IHashids _hashids;

    public BookService(BookRequestValidator bookRequestValidator, IBookRepository bookRepository, IMapper mapper, IHashids hashids)
    {
        _bookRequestValidator = bookRequestValidator;
        _bookRepository = bookRepository;
        _mapper = mapper;
        _hashids = hashids;
    }

    public async Task<OneOf<Book, ValidationFailed>> Create(BookCreateRequest createRequest)
    {
        var validationResult = await _bookRequestValidator.ValidateAsync(createRequest);
        if (!validationResult.IsValid)
            return new ValidationFailed(validationResult.Errors);

        var book = _mapper.Map<Book>(createRequest);
        return await _bookRepository.Add(book);
    }

    public async Task<OneOf<Book, NotFound>> GetById(int id)
    {
        var book = await _bookRepository.GetById(id);
        if (book is null)
            return new NotFound();
        
        return book;
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
        if (bookInDbResponse is null)
            return new NotFound();

        var updateRequest = _mapper.Map<Book>(book);
        var updatedBook = await _bookRepository.Update(updateRequest);
        return updatedBook;
    }

    public async Task<OneOf<Success, NotFound>> DeleteById(int id)
    {
        var isDeleted = await _bookRepository.DeleteById(id);
        return isDeleted ? new Success() : new NotFound();
    }
}