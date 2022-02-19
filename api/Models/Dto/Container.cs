using System;
using System.Collections.Generic;
using System.Text;

namespace api.Models.Dto
{
    public class Container : IEmpty
    {
        public string title { get; set; }
        public List<Container> children { get; set; }

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

            builder.Append(title);
            builder.Append('\n');
            children.ForEach(x => x.Print(tab + 1, builder));
            return builder.ToString();
        }
    }
}