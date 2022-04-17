namespace api.Controllers.Dto;

public class SummarySearchOptions
{
    public string SearchText { get; set; } = string.Empty;
    public bool CaseInsensitive { get; set; } = false;
    public bool TokenizeTitle { get; set; } = false;
}