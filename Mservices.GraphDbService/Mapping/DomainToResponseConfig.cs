using HashidsNet;
using Mapster;
using Mservices.GraphDbService.Data;
using Mservices.GraphDbService.Models;

namespace Mservices.GraphDbService.Mapping;

public class DomainToResponseConfig : IRegister
{
    private readonly IHashids _hashids;

    public DomainToResponseConfig(IHashids hashids)
    {
        _hashids = hashids;
    }

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Book, BookResponse>()
            .Map(x => x.Id, xx => _hashids.Encode(xx.Id));
    }
}