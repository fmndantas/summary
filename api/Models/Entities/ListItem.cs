using System.Text;

namespace api.Models.Entities
{
    public class ListItem : Container<ListItem>
    {
        public string Title { get; set; }

        public override bool IsEmpty()
        {
            return false;
        }

        public override string Print(int tab = 0, StringBuilder builder = null)
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