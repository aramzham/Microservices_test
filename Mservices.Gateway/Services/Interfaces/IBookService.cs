using Mservices.Gateway.Models;

namespace Mservices.Gateway.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetAll();
}