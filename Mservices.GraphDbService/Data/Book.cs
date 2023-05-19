namespace Mservices.GraphDbService.Data;

public class Book
{
    public int Id { get; set; }
    public int Year { get; set; }
    public IEnumerable<string> AuthorNames { get; set; }
    public string Title { get; set; }
}