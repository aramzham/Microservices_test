using Mservices.GraphDbService.Data;
using Mservices.GraphDbService.Models;
using Mservices.GraphDbService.Repositories.Interfaces;
using Neo4j.Driver;
using OneOf;
using OneOf.Types;

namespace Mservices.GraphDbService.Repositories;

public class A
{
    protected string B { get; private set; }

    private List<string> _l = new List<string>();
    
    private int[] C(int[] nums, int k)
    {
        var shift = k % nums.Length;
        if (shift == 0)
            return nums;
        
        var newArray = new int[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            newArray[(i + k) % nums.Length] = nums[i];
        }

        return newArray;
    }
}

public class BookRepository : BaseRepository, IBookRepository
{
    public BookRepository(IDriver driver) : base(driver)
    {
    }
    
    public async Task<Book> Add(Book book)
    {
        await using var session = _driver.AsyncSession();
        var result = await session.ExecuteWriteAsync(async tx =>
            {
                var result = await tx.RunAsync(
                    "CREATE (b:Book) " +
                    "SET b.title = $title " +
                    "RETURN b",
                    new { title = book.Title });

                var single = await result.SingleAsync();
                return single.As<Book>();
            });
        return result;
    }

    public Task<Book?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Book> Update(Book book)
    {
        throw new NotImplementedException();
    }

    public Task<List<Book>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}