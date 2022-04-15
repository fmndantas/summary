using api.Models.Search.Tokenizer;

namespace api.Models.Search.Factory;

public interface ITokenizerFactory<TA, TB>
{
    ITokenizer<TA, TB> MakeTokenizer(TokenizerFactoryOptions options);
}