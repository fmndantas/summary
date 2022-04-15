using System.Collections.Generic;
using api.Models.Dto;

namespace api.Models.Search.Tokenizer;

public interface ITokenizer<TA, TB>
{
    List<Token> GetTokens(TA searchParameters, TB content);
}