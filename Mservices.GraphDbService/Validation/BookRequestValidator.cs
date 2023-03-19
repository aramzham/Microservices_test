using System.Text.RegularExpressions;
using FluentValidation;
using Mservices.GraphDbService.Models;

namespace Mservices.GraphDbService.Validation;

public partial class BookRequestValidator : AbstractValidator<BookRequest>
{
    public BookRequestValidator()
    {
        RuleForEach(x => x.AuthorNames).Matches(AuthorNameRegex());

        RuleFor(x => x.Year).LessThan(DateTime.Now.Year).WithMessage("Year cannot be in the future");
    }

    [GeneratedRegex("^[a-z ,.'-]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled, "en-GB")]
    private static partial Regex AuthorNameRegex();
}
