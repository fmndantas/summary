using System.Collections.Generic;

namespace api.Models.Dto
{
    public class Container
    {
        public string title { get; set; }
        public List<Container> children { get; set; }
    }
}