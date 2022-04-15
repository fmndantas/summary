using System.Collections.Generic;
using api.Models.Dto;
using Newtonsoft.Json;
using NUnit.Framework;

namespace tests.ServicesTests.LearningTests;

public class TestContainerDeserialization
{
    [Test]
    public void TestSimpleContainerDeserialization()
    {
        var originalRoot = new Container
        {
            Title = "1",
            Children = new List<Container>
            {
                new Container
                {
                    Title = "11",
                    Children = new List<Container>()
                },
                new Container
                {
                    Title = "12",
                    Children = new List<Container>()
                }
            }
        };

        var serializedOriginalRoot = JsonConvert.SerializeObject(originalRoot);
        
        var deserializedRoot = JsonConvert.DeserializeObject<Container>(serializedOriginalRoot);
        
        Assert.AreEqual(originalRoot.Title, deserializedRoot?.Title);
        Assert.AreEqual(originalRoot.Children[0].Title, deserializedRoot?.Children[0].Title);
        Assert.AreEqual(originalRoot.Children[1].Title, deserializedRoot?.Children[1].Title);
        Assert.AreEqual(originalRoot.Children.Count, deserializedRoot?.Children.Count);
        Assert.AreEqual(0, originalRoot.Children[0].Children.Count);
        Assert.AreEqual(0, originalRoot.Children[1].Children.Count);
    }
}