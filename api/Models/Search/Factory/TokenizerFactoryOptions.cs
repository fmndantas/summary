using api.Controllers.Dto;
using api.Models.Enum;

namespace api.Models.Search.Factory;

public class TokenizerFactoryOptions
{
    public TokenizerFactoryOptions(SummarySearchOptions options)
    {
        TokenizerType = options.CaseInsensitive
            ? TokenizerType.CaseInsensitive
            : TokenizerType.CaseSensitive;
    }

    public TokenizerType TokenizerType { get; set; }
}