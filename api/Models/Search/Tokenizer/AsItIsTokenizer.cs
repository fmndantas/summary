using System.Collections.Generic;
using api.Models.Entities;

namespace api.Models.Search.Tokenizer;

public class AsItIsTokenizer : ITokenizer
{
    public List<Token> GetTokens(string searchText, string content)
    {
        return new List<Token> { new Token(content, false) };
    }
}