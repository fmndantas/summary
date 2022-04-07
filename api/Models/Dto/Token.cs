namespace api.Models.Dto;

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