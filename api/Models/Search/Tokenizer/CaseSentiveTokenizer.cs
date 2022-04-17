using System.Collections.Generic;
using api.Models.Entities;

namespace api.Models.Search.Tokenizer;

public class CaseSentiveTokenizer : ITokenizer
{
    public List<Token> GetTokens(string searchText, string content)
    {
        content = content
            .Replace(searchText, "~");
        var start = 0;
        var tokens = new List<Token>();
        for (var i = 0; i < content.Length; ++i)
        {
            if (content[i] == '~')
            {
                if (i - start > 0)
                {
                    tokens.Add(new Token(content.Substring(start, i - start), false));
                }

                tokens.Add(new Token(searchText, true));

                start = i + 1;
            }
            else
            {
                if (i == content.Length - 1)
                {
                    tokens.Add(new Token(content.Substring(start, i - start + 1), false));
                }
            }
        }

        return tokens;
    }
}