using api.Models.Utils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace tests.ServicesTests.LearningTests;

public class TestSerializationSettings
{
    [Test]
    public void OnlySerializedContainerShouldBeSerialized()
    {
        var summary = SummaryGenerator.Generate(2, 5, 1);
        var serialization = JsonConvert.SerializeObject(summary);
        Assert.False(serialization.Contains("\"Root\""));
    }
}