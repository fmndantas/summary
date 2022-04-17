using System.Collections.Generic;
using System.Linq;
using api.Models.Entities;
using api.Models.Search.Tokenizer;
using NUnit.Framework;

namespace tests.SearchTests;

public class TestCaseInsensitiveTokenizer
{
    private CaseInsensitiveTokenizer _subject = new();

    private List<int> SelectedTokens(List<Token> tokens)
    {
        return tokens
            .Select((x, i) => new { position = i, token = x })
            .Where(x => x.token.Selected)
            .Select(x => x.position)
            .ToList();
    }

    [Test]
    public void TestCase1()
    {
        var content = "wOrdWordwoRD7";
        var search = "word";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(4, tokens.Count);
        Assert.AreEqual("wOrd", tokens[0].Content);
        Assert.AreEqual("Word", tokens[1].Content);
        Assert.AreEqual("woRD", tokens[2].Content);
        Assert.AreEqual("7", tokens[3].Content);
        Assert.AreEqual(new List<int> { 0, 1, 2 }, SelectedTokens(tokens));
    }

    [Test]
    public void TestCase2()
    {
        var content = "wOrdWordwoRD7";
        var search = "abcd";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(1, tokens.Count);
        Assert.AreEqual(content, tokens[0].Content);
        Assert.False(tokens[0].Selected);
    }

    [Test]
    public void TestCase3()
    {
        var content = "wOrdWordwoRD7";
        var search = "7";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(2, tokens.Count);
        Assert.AreEqual("wOrdWordwoRD", tokens[0].Content);
        Assert.AreEqual("7", tokens[1].Content);
        Assert.AreEqual(new List<int> { 1 }, SelectedTokens(tokens));
    }

    [Test]
    public void TestCase4()
    {
        var content = "wOrdWordwoRD7wOrdWordwoRD";
        var search = "7";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(3, tokens.Count);
        Assert.AreEqual("wOrdWordwoRD", tokens[0].Content);
        Assert.AreEqual("7", tokens[1].Content);
        Assert.AreEqual("wOrdWordwoRD", tokens[2].Content);
        Assert.AreEqual(new List<int> { 1 }, SelectedTokens(tokens));
    }

    [Test]
    public void TestCase5()
    {
        var content = "abcABCaBcAbC";
        var search = "AbC";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(4, tokens.Count);
        Assert.True(tokens.All(x => x.Selected));
        Assert.True(tokens.All(x => x.Content.ToLower().Equals("abc")));
    }

    [Test]
    public void TestCase6()
    {
        var content = "abcABCaBcAbC1";
        var search = "AbC";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(5, tokens.Count);
        Assert.AreEqual("1", tokens[^1].Content);
        tokens = tokens.GetRange(0, tokens.Count - 1);
        Assert.True(tokens.All(x => x.Selected));
        Assert.True(tokens.All(x => x.Content.ToLower().Equals("abc")));
    }

    [Test]
    public void TestCase7()
    {
        var content = "this is the name of the book";
        var search = "book";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(2, tokens.Count);
        Assert.AreEqual("this is the name of the ", tokens[0].Content);
        Assert.AreEqual("book", tokens[1].Content);
        Assert.AreEqual(new List<int> { 1 }, SelectedTokens(tokens));
    }

    [Test]
    public void TestCase8()
    {
        var content = "123aaa456bbb789ccc1a1b1c";
        var search = "A";
        var tokens = _subject.GetTokens(search, content);
        Assert.AreEqual(7, tokens.Count);
        Assert.AreEqual("123", tokens[0].Content);
        Assert.AreEqual("a", tokens[1].Content);
        Assert.AreEqual("a", tokens[2].Content);
        Assert.AreEqual("a", tokens[3].Content);
        Assert.AreEqual("456bbb789ccc1", tokens[4].Content);
        Assert.AreEqual("a", tokens[5].Content);
        Assert.AreEqual("1b1c", tokens[6].Content);
        Assert.AreEqual(new List<int> { 1, 2, 3, 5 }, SelectedTokens(tokens));
    }
}