using api.Models.Enum;
using api.Models.Search.Tokenizer;

namespace api.Models.Search.Factory;

public class SsTokenizerFactory : ITokenizerFactory<string, string>
{
    public ITokenizer<string, string> MakeTokenizer(TokenizerFactoryOptions options)
    {
        if (options.TokenizerType == TokenizerType.CaseSensitive)
        {
            return new CaseSentiveTokenizer();
        }

        return new CaseInsensitiveTokenizer();
    }
}