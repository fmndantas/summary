using api.Models.Dto;
using api.Models.Search.Tokenizer;

namespace api.Models.Entities;

public class TokenizedTitleSummary
{
    public string Title { get; }

    public TokenizedTitleContainer Root { get; }

    public TokenizedTitleSummary(
        Summary summary,
        string searchText,
        ITokenizer<string, string> tokenizer)
    {
        Title = summary.Title;
        var tokenizedTitleContainer = new TokenizedTitleContainer(tokenizer);
        tokenizedTitleContainer.Search(summary.Root, searchText);
        Root = tokenizedTitleContainer;
    }

    public bool HasAnyResult => Root.HasAnyResult;
}