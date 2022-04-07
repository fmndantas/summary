using System.Collections.Generic;
using System.Linq;
using api.Models.Search.Tokenizer;
using Newtonsoft.Json;

namespace api.Models.Dto;

public class TokenizedTitleContainer
{
    public TokenizedTitleContainer(ITokenizer<string, string> tokenizer)
    {
        Children = new List<TokenizedTitleContainer>();
        Title = new List<Token>();
        _tokenizer = tokenizer;
    }

    private ITokenizer<string, string> _tokenizer;

    public List<Token> Title { get; set; }

    public List<TokenizedTitleContainer> Children { get; set; }

    [JsonIgnore] public int ChildrenCounting => Children.Count;

    public bool HasAnyResult => ChildrenCounting > 0
                                || ChildrenCounting == 0 && Title.Any(x => x.Selected);

    public void Search(Container container, string searchParameters)
    {
        foreach (var child in container.Children)
        {
            SetupChild(this, child, searchParameters);
        }

        SetupTokens(container, searchParameters);
    }

    private void SetupChild(
        TokenizedTitleContainer parentSearchContainer,
        Container container,
        string searchParameters
    )
    {
        var searchChild = new TokenizedTitleContainer(_tokenizer);
        foreach (var child in container.Children)
        {
            SetupChild(searchChild, child, searchParameters);
        }

        searchChild.SetupTokens(container, searchParameters);
        if (searchChild.HasAnyResult)
        {
            parentSearchContainer.Children.Add(searchChild);
        }
    }

    private void SetupTokens(Container container, string searchParameters)
    {
        Title = _tokenizer.GetTokens(searchParameters, container.Title);
    }
}