namespace WebSummary.InfoEngine;

public struct PageLink(string url, PageLink.LinkType type)
{
  public enum LinkType
  {
    SamePage,
    SameSite,
    External
    //TODO: Add special types like wikipedia, youtube etc.
  }


  public string Url { get; } = url;
  public LinkType Type { get; } = type;
}
