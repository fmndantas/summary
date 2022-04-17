namespace api.Models.Entities;

public class Token
{
    public readonly string Content;
    public readonly bool Selected;

    public Token(string content, bool selected)
    {
        Content = content;
        Selected = selected;
    }
}