using api.Controllers.Dto;
using api.Models.Search.Tokenizer;

namespace api.Models.Entities;

public class TokenizedSummary
{
    public string Title { get; }

    public TokenizedListItem Root { get; }

    public TokenizedSummary(
        Summary summary,
        SummarySearchOptions searchOptions,
        ITokenizer tokenizer
    )
    {
        Title = summary.Title;
        var tokenizedListItem = new TokenizedListItem(tokenizer);
        tokenizedListItem.Search(summary.Root, searchOptions);
        Root = tokenizedListItem;
    }

    public bool HasAnyResult => Root.HasAnyResult;
}