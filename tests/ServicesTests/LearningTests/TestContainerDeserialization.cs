using System.Collections.Generic;
using api.Models.Dto;
using NUnit.Framework;
using Newtonsoft.Json;

namespace tests.ServicesTests.LearningTests;

public class TestContainerDeserialization
{
    [Test]
    public void TestSimpleContainerDeserialization()
    {
        var originalRoot = new Container
        {
            title = "1",
            children = new List<Container>
            {
                new Container
                {
                    title = "11",
                    children = new List<Container>()
                },
                new Container
                {
                    title = "12",
                    children = new List<Container>()
                }
            }
        };

        var serializedOriginalRoot = JsonConvert.SerializeObject(originalRoot);
        
        var deserializedRoot = JsonConvert.DeserializeObject<Container>(serializedOriginalRoot);
        
        Assert.AreEqual(originalRoot.title, deserializedRoot?.title);
        Assert.AreEqual(originalRoot.children[0].title, deserializedRoot?.children[0].title);
        Assert.AreEqual(originalRoot.children[1].title, deserializedRoot?.children[1].title);
        Assert.AreEqual(originalRoot.children.Count, deserializedRoot?.children.Count);
        Assert.AreEqual(0, originalRoot.children[0].children.Count);
        Assert.AreEqual(0, originalRoot.children[1].children.Count);
    }
}