namespace Mservices.GraphDbService.Models;

// book
public abstract record BookRequest(string Title, IEnumerable<string> AuthorNames, int Year);
public record BookUpdateRequest(Guid Id, string Title, IEnumerable<string> AuthorNames, int Year) : BookRequest(Title, AuthorNames, Year);
public record BookCreateRequest(string Title, IEnumerable<string> AuthorNames, int Year) : BookRequest(Title, AuthorNames, Year);
// author
public record AuthorRequest(string Name);