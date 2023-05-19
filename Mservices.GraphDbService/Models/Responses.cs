namespace Mservices.GraphDbService.Models;

public record BookResponse(string Id, string Title, IEnumerable<string> AuthorNames, int Year);