using Mapster;
using Mservices.GraphDbService.Data;
using Mservices.GraphDbService.Models;

namespace Mservices.GraphDbService.Mapping;

public class RequestToDomainConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BookUpdateRequest, Book>();
        config.NewConfig<BookCreateRequest, Book>();
    }
}