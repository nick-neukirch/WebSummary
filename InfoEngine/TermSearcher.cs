using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Google.Apis.Services;

namespace WebSummary.InfoEngine;

public class TermSearcher 
{
  private string _searchEngineId;
  private CustomSearchAPIService _searchService;
  
  
  public TermSearcher() 
  {
    _searchEngineId = Environment.GetEnvironmentVariable("WEBSUMMARY_SEARCH_ENGINE_ID") ?? string.Empty;
    string googleApiKey = Environment.GetEnvironmentVariable("WEBSUMMARY_GOOGLE_API_KEY") ?? string.Empty;

    _searchService = new CustomSearchAPIService(new BaseClientService.Initializer()
    {
      ApplicationName = "WebSummary",
      ApiKey = googleApiKey
    });
  }


  public IList<Result> GetResultsForTerm(string searchTerm, int resultCount = 3)
  {
    CseResource.ListRequest? listRequest = _searchService.Cse.List();
    listRequest.Cx = _searchEngineId;
    listRequest.Q = searchTerm;
    listRequest.Num = resultCount;

    return listRequest.Execute().Items;
  }
}