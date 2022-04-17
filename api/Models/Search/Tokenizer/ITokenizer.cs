using System.Collections.Generic;
using api.Models.Entities;

namespace api.Models.Search.Tokenizer;

public interface ITokenizer
{
    List<Token> GetTokens(string searchText, string content);
}