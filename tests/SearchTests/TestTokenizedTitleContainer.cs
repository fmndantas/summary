using System;
using System.Collections.Generic;
using System.Text.Json;
using api.Models.Dto;
using api.Models.Entities;
using api.Models.Search.Tokenizer;
using NUnit.Framework;

namespace tests.SearchTests;

public class TestTokenizedTitleContainer
{
    private Summary _subject;

    [SetUp]
    public void SetUp()
    {
        var root = new Container
        {
            Title = "word1",
            Children = new List<Container>
            {
                new Container { Title = "word2" },
                new Container
                {
                    Title = "word3",
                    Children = new List<Container>
                    {
                        new Container { Title = "word4" },
                        new Container { Title = "word5" }
                    }
                },
                new Container { Title = "word6" },
                new Container { Title = "wordwordword7" }
            }
        };
        _subject = new Summary
        {
            SerializedRoot = JsonSerializer.Serialize(root)
        };
    }

    [Test]
    public void TestRootContainer()
    {
        var container = _subject.Root;
        Assert.NotNull(container);
        Assert.AreEqual(4, container.ChildrenCounting);
    }

    /// <summary>
    /// Only root SearchContainer should return an result
    /// </summary>
    [Test]
    public void TestSearchWithWord1()
    {
        var searchContainer = new TokenizedTitleContainer(new CaseSentiveTokenizer());
        searchContainer.Search(_subject.Root, "ord1");
        Assert.AreEqual(0, searchContainer.ChildrenCounting);
    }

    /// <summary>
    /// All containers should be returned
    /// </summary>
    [Test]
    public void TestSearchWithOrd()
    {
        var searchContainer = new TokenizedTitleContainer(new CaseSentiveTokenizer());
        searchContainer.Search(_subject.Root, "ord");
        Assert.AreEqual(4, searchContainer.ChildrenCounting);
    }

    /// <summary>
    /// Only the last container should be returned
    /// </summary>
    [Test]
    public void TestSearchWithOrdw()
    {
        var searchContainer = new TokenizedTitleContainer(new CaseSentiveTokenizer());
        searchContainer.Search(_subject.Root, "ordw");
        Assert.AreEqual(1, searchContainer.ChildrenCounting);
        Assert.AreEqual(1, searchContainer.Title.Count);
        Assert.AreEqual(4, searchContainer.Children[0].Title.Count);
    }

    [Test]
    public void TestSearchWithWord5()
    {
        var searchContainer = new TokenizedTitleContainer(new CaseSentiveTokenizer());
        searchContainer.Search(_subject.Root, "word5");
        Assert.AreEqual(1, searchContainer.ChildrenCounting);
        Assert.AreEqual(1, searchContainer.Children[0].ChildrenCounting);
        Assert.AreEqual(0, searchContainer.Children[0].Children[0].ChildrenCounting);
        Assert.AreEqual(1, searchContainer.Children[0].Children[0].Title.Count);
        Assert.AreEqual("word5", searchContainer.Children[0].Children[0].Title[0].Content);
        Assert.AreEqual("word3", searchContainer.Children[0].Title[0].Content);
        Assert.AreEqual("word1", searchContainer.Title[0].Content);
    }
}