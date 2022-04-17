using api.Models.Enum;
using api.Models.Search.Tokenizer;

namespace api.Models.Search.Factory;

public class StringStringTokenizerFactory : ITokenizerFactory
{
    public ITokenizer MakeTokenizer(TokenizerFactoryOptions options)
    {
        if (options.TokenizerType == TokenizerType.CaseSensitive)
        {
            return new CaseSentiveTokenizer();
        }

        return new CaseInsensitiveTokenizer();
    }
}