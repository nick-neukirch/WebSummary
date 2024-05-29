namespace WebSummary.InfoEngine;

public class PageData(string url,
                      string title,
                      IEnumerable<PageLink> links,
                      IEnumerable<PageParagraph> paragraphs,
                      IEnumerable<PageImage> images)
{
  public string Url { get; set; } = url;
  public string Title { get; set; } = title;
  public IEnumerable<PageLink> Links { get; set; } = links;
  public IEnumerable<PageParagraph> Paragraphs { get; set; } = paragraphs;
  public IEnumerable<PageImage> Images { get; set; } = images;
}