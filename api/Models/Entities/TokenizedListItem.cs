using System.Collections.Generic;
using System.Linq;
using api.Controllers.Dto;
using api.Models.Search.Tokenizer;
using Newtonsoft.Json;

namespace api.Models.Entities;

public class TokenizedListItem
{
    public TokenizedListItem(ITokenizer tokenizer)
    {
        Children = new List<TokenizedListItem>();
        Title = new List<Token>();
        _tokenizer = tokenizer;
        _asItIsTokenizer = new AsItIsTokenizer();
    }

    private ITokenizer _tokenizer;
    private ITokenizer _asItIsTokenizer;
    
    public List<Token> Title { get; set; }

    public List<TokenizedListItem> Children { get; set; }

    [JsonIgnore] public int ChildrenCounting => Children.Count;

    public bool HasAnyResult => ChildrenCounting > 0
                                || ChildrenCounting == 0 && Title.Any(x => x.Selected);

    public void Search(ListItem listItem, SummarySearchOptions searchParameters)
    {
        foreach (var child in listItem.Children)
        {
            SetupChild(this, child, searchParameters);
        }

        SetupTokens(listItem, searchParameters);
    }

    private void SetupChild(
        TokenizedListItem parentSearchListItem,
        ListItem listItem,
        SummarySearchOptions searchParameters
    )
    {
        var searchChild = new TokenizedListItem(_tokenizer);
        foreach (var child in listItem.Children)
        {
            SetupChild(searchChild, child, searchParameters);
        }

        searchChild.SetupTokens(listItem, searchParameters);
        if (searchChild.HasAnyResult)
        {
            parentSearchListItem.Children.Add(searchChild);
        }
    }

    private void SetupTokens(ListItem listItem, SummarySearchOptions searchParameters)
    {
        Title = searchParameters.TokenizeTitle
            ? _tokenizer.GetTokens(searchParameters.SearchText, listItem.Title)
            : _asItIsTokenizer.GetTokens(string.Empty, listItem.Title);
    }
}