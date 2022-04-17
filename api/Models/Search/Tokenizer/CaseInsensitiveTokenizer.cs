using System.Collections.Generic;
using api.Models.Entities;

namespace api.Models.Search.Tokenizer;

public class CaseInsensitiveTokenizer : ITokenizer
{
    public List<Token> GetTokens(string searchParameters, string content)
    {
        var lowerSearchParameters = searchParameters.ToLower();
        var lowerContent = content.ToLower();
        var contentLength = lowerContent.Length;
        var searchParametersLength = searchParameters.Length;
        var starts = GetStarts(lowerSearchParameters, lowerContent);
        var tokens = new List<Token>();
        var i = 0;
        var j = 0;
        while (starts.Count > 0)
        {
            if (i == starts.Peek())
            {
                starts.Dequeue();
                if (j < i)
                {
                    tokens.Add(new Token(content.Substring(j, i - j), false));
                }

                tokens.Add(new Token(content.Substring(i, searchParametersLength), true));
                i += searchParametersLength;
                j = i;
            }
            else
            {
                ++i;
            }
        }
        
        if (i < contentLength)
        {
            tokens.Add(new Token(content.Substring(i, contentLength - i), false));
        }

        return tokens;
    }

    private Queue<int> GetStarts(string lowerSearchParameters, string lowerContent)
    {
        var searchLength = lowerSearchParameters.Length;
        var starts = new Queue<int>();
        var j = 0;
        for (var i = 0; i < lowerContent.Length; ++i)
        {
            if (lowerContent[i] == lowerSearchParameters[j])
            {
                j++;
            }
            else
            {
                j = 0;
            }
            
            if (j == lowerSearchParameters.Length)
            {
                starts.Enqueue(i - searchLength + 1);
                j = 0;
            }
        }

        return starts;
    }
}