using api.Models.Search.Tokenizer;
using NUnit.Framework;

namespace tests.SearchTests;

public class TestAsItIsTokenizer
{
    private AsItIsTokenizer _subject = new();
    
    [Test]
    public void TeestAsItIsTokenizer()
    {
        var tokens = _subject.GetTokens("does", "it does not matter");
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(false, tokens[0].Selected);
    }
}