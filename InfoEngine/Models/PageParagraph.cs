namespace WebSummary.InfoEngine;

public struct PageParagraph(string content)
{
    //TODO: Paragraph titles?
    public string Content { get; } = content;
}
