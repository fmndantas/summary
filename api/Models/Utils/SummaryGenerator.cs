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
        public static Summary Generate(int nodes, int titleLength)
        {
            return new Summary
            {
                Title = GenerateWord(titleLength),
                Author = GenerateWord(titleLength),
                Root = JsonConvert.SerializeObject(GenerateContent(nodes, titleLength, 2))
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

        private static Container GenerateContent(int nodes, int titleLength, int maximumDeep = 1)
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
                title = GenerateWord(titleLength),
                children = new List<Container>()
            };

            foreach (var path in paths)
            {
                var currentNode = container;

                if (path.Count == 0)
                {
                    currentNode.children.Add(new Container
                    {
                        title = GenerateWord(titleLength),
                        children = new List<Container>()
                    });
                }

                for (var j = 0; j < path.Count; ++j)
                {
                    var next = path[j];
                    var toAdd = next - currentNode.children.Count + 1;

                    for (var i = 0; i < Math.Max(0, toAdd); ++i)
                    {
                        currentNode.children.Add(new Container
                        {
                            title = GenerateWord(titleLength),
                            children = new List<Container>()
                        });
                    }

                    if (j == path.Count - 1)
                    {
                        currentNode.children[next].children.Add(new Container
                        {
                            title = GenerateWord(titleLength),
                            children = new List<Container>()
                        });
                    }

                    currentNode = currentNode.children[next];
                }
            }

            return container;
        }
    }
}