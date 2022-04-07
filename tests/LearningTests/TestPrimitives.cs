using System;
using NUnit.Framework;

namespace tests.ServicesTests.LearningTests;

public class TestPrimitives
{
    [Test]
    public void TestStringSplit()
    {
        var s = "word1";
        var tokens = s.Split("or");
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }
}