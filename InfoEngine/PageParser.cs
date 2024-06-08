using HtmlAgilityPack;

namespace WebSummary.InfoEngine;

public class PageParser
{
  private const string _titleNodePath = "/head/title";
  private const string _anchorNodePath = "/body//a";
  private const string _imageNodePath = "/body//img";
  private const string _paragraphNodePath = "/body//p";


  public static async Task<PageData?> ParsePageFromUrl(string url)
  {
    HtmlDocument document = new();
    using(HttpClient client = new()) 
    {
      HttpResponseMessage response = await client.GetAsync(url);

      if(!response.IsSuccessStatusCode) return null;

      document.LoadHtml(await response.Content.ReadAsStringAsync());
    }

    string pageTitle = document.DocumentNode.SelectSingleNode(_titleNodePath).InnerText;
    IEnumerable<PageLink> links = FilterLinks(document);
    IEnumerable<PageParagraph> paragraphs = FilterParagraphs(document);
    IEnumerable<PageImage> images = FilterImages(document);

    return new PageData(url, pageTitle, links, paragraphs, images);
  }

  private static IEnumerable<PageLink> FilterLinks(HtmlDocument document)
  {
    List<PageLink> links = [];

    foreach(HtmlNode node in document.DocumentNode.SelectNodes(_anchorNodePath))
    {
      if(!node.Attributes.Contains("href")) continue;
      //FIXME: Make links more uniform in order to make the system more robust.
      string url = node.Attributes["href"].Value;

      //TODO: Improve link type detection.
      if(url.StartsWith('#'))
      {
        links.Add(new PageLink(url, PageLink.LinkType.SamePage));
      }
      else if(url.StartsWith('/'))
      {
        links.Add(new PageLink(url, PageLink.LinkType.SameSite));
      }
      else if(url.StartsWith("http"))
      {
        links.Add(new PageLink(url, PageLink.LinkType.External));
      }
    }

    return links;
  }

  private static IEnumerable<PageParagraph> FilterParagraphs(HtmlDocument document)
  {
    List<PageParagraph> paragraphs = [];

    foreach(HtmlNode node in document.DocumentNode.SelectNodes(_paragraphNodePath))
    {
      //TODO: Better paragraph detection
      paragraphs.Add(new PageParagraph(node.InnerText));
    }

    return paragraphs;
  }

  private static IEnumerable<PageImage> FilterImages(HtmlDocument document)
  {
    List<PageImage> images = [];

    foreach(HtmlNode node in document.DocumentNode.SelectNodes(_imageNodePath))
    {
      if(node.Attributes.Contains("src")) continue;
      //FIXME: Make links more uniform in order to make the system more robust.
      string url = node.Attributes["src"].Value;

      images.Add(new PageImage(url));
    }

    return images;
  }
}