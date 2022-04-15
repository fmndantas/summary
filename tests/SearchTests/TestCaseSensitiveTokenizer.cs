using api.Models.Search;
using api.Models.Search.Tokenizer;
using NUnit.Framework;

namespace tests.SearchTests;

public class TestCaseSensitiveTokenizer
{
    private CaseSentiveTokenizer _subject = new();

    [Test]
    public void TestCase1()
    {
        var content = "wordwordword7";
        var search = "ordw";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual("w", tokens[0].Content);
        Assert.False(tokens[0].Selected);
        Assert.AreEqual("ordw", tokens[1].Content);
        Assert.True(tokens[1].Selected);
        Assert.AreEqual("ordw", tokens[2].Content);
        Assert.True(tokens[2].Selected);
        Assert.AreEqual("ord7", tokens[3].Content);
        Assert.False(tokens[3].Selected);
    }

    [Test]
    public void TestCase2()
    {
        var content = "word";
        var search = "word";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual("word", tokens[0].Content);
        Assert.True(tokens[0].Selected);
    }

    [Test]
    public void TestCase3()
    {
        var content = "abababababab";
        var search = "a";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(12, tokens.Count);
        for (var i = 0; i < tokens.Count; ++i)
        {
            if (i % 2 == 0)
            {
                Assert.AreEqual("a", tokens[i].Content);
                Assert.True(tokens[i].Selected);
            }
            else
            {
                Assert.AreEqual("b", tokens[i].Content);
                Assert.False(tokens[i].Selected);
            }
        }
    }

    [Test]
    public void TestCase4()
    {
        var content = "abababababab";
        var search = "b";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(12, tokens.Count);
        for (var i = 0; i < tokens.Count; ++i)
        {
            if (i % 2 == 0)
            {
                Assert.AreEqual("a", tokens[i].Content);
                Assert.False(tokens[i].Selected);
            }
            else
            {
                Assert.AreEqual("b", tokens[i].Content);
                Assert.True(tokens[i].Selected);
            }
        }
    }

    [Test]
    public void TestCase5()
    {
        var content = "w or  d   w    ord";
        var search = "w";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual("w", tokens[0].Content);
        Assert.True(tokens[0].Selected);
        Assert.AreEqual(" or  d   ", tokens[1].Content);
        Assert.False(tokens[1].Selected);
        Assert.AreEqual("w", tokens[2].Content);
        Assert.True(tokens[2].Selected);
        Assert.AreEqual("    ord", tokens[3].Content);
        Assert.False(tokens[3].Selected);
    }

    [Test]
    public void TestCase6()
    {
        var content = "Patterns";
        var search = "pattern";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual("Patterns", tokens[0].Content);
        Assert.False(tokens[0].Selected);
    }
}