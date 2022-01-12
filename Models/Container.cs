using System.Collections.Generic;

namespace summary.Models
{
    public class Container
    {
        public string title { get; set; }
        public List<Container> children { get; set; }
    }
}