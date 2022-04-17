using System.Text.Json;
using api.Controllers.Dto;
using api.Models.Entities;
using api.Models.Search.Tokenizer;
using NUnit.Framework;

namespace tests.SearchTests;

public class TestTokenizedListItem
{
    private Summary _subject;

    [SetUp]
    public void SetUp()
    {
        var root = new ListItem
        {
            Title = "word1"
        };
        root.AddChild(new ListItem { Title = "word2" });
        root.AddChild(new ListItem { Title = "word6" });
        root.AddChild(new ListItem { Title = "wordwordword7" });
        var childWord3 = new ListItem { Title = "word3" };
        childWord3.AddChild(new ListItem { Title = "word4" });
        childWord3.AddChild(new ListItem { Title = "word5" });
        root.AddChild(childWord3);
        _subject = new Summary
        {
            SerializedRoot = JsonSerializer.Serialize(root)
        };
    }

    [Test]
    public void TestRootContainer()
    {
        var listItem = _subject.Root;
        Assert.NotNull(listItem);
        Assert.AreEqual(4, listItem.ChildrenCounting);
    }

    /// <summary>
    /// Only root should return an result
    /// </summary>
    [Test]
    public void TestSearchWithWord1()
    {
        var tokenizedListItem = new TokenizedListItem(new CaseSentiveTokenizer());
        tokenizedListItem.Search(_subject.Root, new SummarySearchOptions { SearchText = "ord1", TokenizeTitle = true });
        Assert.AreEqual(0, tokenizedListItem.ChildrenCounting);
    }

    /// <summary>
    /// All items should be returned
    /// </summary>
    [Test]
    public void TestSearchWithOrd()
    {
        var tokenizedListItem = new TokenizedListItem(new CaseSentiveTokenizer());
        tokenizedListItem.Search(_subject.Root, new SummarySearchOptions { SearchText = "ord", TokenizeTitle = true });
        Assert.AreEqual(4, tokenizedListItem.ChildrenCounting);
    }

    /// <summary>
    /// No items should be returned
    /// </summary>
    [Test]
    public void TestSearchWithOrdWithoutTitleTokenization()
    {
        var tokenizedListItem = new TokenizedListItem(new CaseSentiveTokenizer());
        tokenizedListItem.Search(_subject.Root, new SummarySearchOptions { SearchText = "ord", TokenizeTitle = false });
        Assert.AreEqual(0, tokenizedListItem.ChildrenCounting);
    }

    /// <summary>
    /// Only the last item should be returned
    /// </summary>
    [Test]
    public void TestSearchWithOrdw()
    {
        var tokenizedListItem = new TokenizedListItem(new CaseSentiveTokenizer());
        tokenizedListItem.Search(_subject.Root, new SummarySearchOptions { SearchText = "ordw", TokenizeTitle = true });
        Assert.AreEqual(1, tokenizedListItem.ChildrenCounting);
        Assert.AreEqual(1, tokenizedListItem.Title.Count);
        Assert.AreEqual(4, tokenizedListItem.Children[0].Title.Count);
    }

    [Test]
    public void TestSearchWithWord5()
    {
        var tokenizedListItem = new TokenizedListItem(new CaseSentiveTokenizer());
        tokenizedListItem.Search(_subject.Root,
            new SummarySearchOptions { SearchText = "word5", TokenizeTitle = true });
        Assert.AreEqual(1, tokenizedListItem.ChildrenCounting);
        Assert.AreEqual(1, tokenizedListItem.Children[0].ChildrenCounting);
        Assert.AreEqual(0, tokenizedListItem.Children[0].Children[0].ChildrenCounting);
        Assert.AreEqual(1, tokenizedListItem.Children[0].Children[0].Title.Count);
        Assert.AreEqual("word5", tokenizedListItem.Children[0].Children[0].Title[0].Content);
        Assert.AreEqual("word3", tokenizedListItem.Children[0].Title[0].Content);
        Assert.AreEqual("word1", tokenizedListItem.Title[0].Content);
    }
}