using Mservices.GraphDbService.Repositories.Interfaces;
using Neo4j.Driver;

namespace Mservices.GraphDbService.Repositories;

public class BaseRepository : IBaseRepository, IDisposable
{
    protected readonly IDriver _driver;

    public BaseRepository(IDriver driver)
    {
        _driver = driver;
    }

    public void Dispose()
    {
        _driver.Dispose();
    }
}