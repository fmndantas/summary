using System.Text;

namespace api.Models.Entities;

public class EmptyListItem : ListItem
{
    public override bool IsEmpty()
    {
        return true;
    }

    public override string Print(int tab = 0, StringBuilder builder = null)
    {
        return string.Empty;
    }
}