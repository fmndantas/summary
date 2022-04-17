using api.Models.Search.Tokenizer;

namespace api.Models.Search.Factory;

public interface ITokenizerFactory
{
    ITokenizer MakeTokenizer(TokenizerFactoryOptions options);
}