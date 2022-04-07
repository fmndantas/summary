using System;
using System.Collections.Generic;
using System.Linq;
using api.Models.Dto;
using api.Models.Entities;
using Newtonsoft.Json;

namespace api.Models.Utils
{
    public static class SummaryGenerator
    {
        public static Summary Generate(int nodes, int titleLength, int maximumDeep = 2)
        {
            return new Summary
            {
                Title = GenerateWord(titleLength),
                Author = GenerateWord(titleLength),
                SerializedRoot = JsonConvert.SerializeObject(GenerateContainer(nodes, titleLength, maximumDeep))
            };
        }

        private static string GenerateWord(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }

        private static Container GenerateContainer(int nodes, int titleLength, int maximumDeep = 1)
        {
            var random = new Random();

            var paths = new List<List<int>>();

            for (var i = 0; i < nodes; ++i)
            {
                var pathLength = random.Next(maximumDeep);
                var path = new List<int>();
                for (var j = 0; j < pathLength; ++j)
                {
                    path.Add(random.Next(4));
                }
            
                paths.Add(path);
            }

            return GenerateContainerFromPaths(paths, titleLength);
        }

        private static Container GenerateContainerFromPaths(List<List<int>> paths, int titleLength)
        {
            var container = new Container
            {
                Title = GenerateWord(titleLength),
                Children = new List<Container>()
            };

            foreach (var path in paths)
            {
                var currentNode = container;

                if (path.Count == 0)
                {
                    currentNode.Children.Add(new Container
                    {
                        Title = GenerateWord(titleLength),
                        Children = new List<Container>()
                    });
                }

                for (var j = 0; j < path.Count; ++j)
                {
                    var next = path[j];
                    var toAdd = next - currentNode.Children.Count + 1;

                    for (var i = 0; i < Math.Max(0, toAdd); ++i)
                    {
                        currentNode.Children.Add(new Container
                        {
                            Title = GenerateWord(titleLength),
                            Children = new List<Container>()
                        });
                    }

                    if (j == path.Count - 1)
                    {
                        currentNode.Children[next].Children.Add(new Container
                        {
                            Title = GenerateWord(titleLength),
                            Children = new List<Container>()
                        });
                    }

                    currentNode = currentNode.Children[next];
                }
            }

            return container;
        }
    }
}