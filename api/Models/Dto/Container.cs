using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace api.Models.Dto
{
    public class Container : IEmpty
    {
        public Container()
        {
            Children = new List<Container>();
        }
        
        public string Title { get; set; }
        public List<Container> Children { get; set; }

        [JsonIgnore]
        public int ChildrenCounting => Children.Count;
        
        public virtual bool isEmpty()
        {
            return false;
        }

        public string Print(int tab = 0, StringBuilder builder = null)
        {
            if (builder == null)
            {
                builder = new StringBuilder();
            }

            for (var i = 0; i < tab; ++i)
            {
                builder.Append('\t');
            }

            builder.Append(Title);
            builder.Append('\n');
            Children.ForEach(x => x.Print(tab + 1, builder));
            return builder.ToString();
        }
    }
}