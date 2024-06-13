using Google.Apis.CustomSearchAPI.v1;
using Google.Apis.CustomSearchAPI.v1.Data;
using Google.Apis.Services;

namespace WebSummary.InfoEngine;

public class TermSearcher 
{
  public const string SearchEngineIdVariable = "WEBSUMMARY_SEARCH_ENGINE_ID";
  public const string GoogleApiKeyVariable = "WEBSUMMARY_GOOGLE_API_KEY";

  private string _searchEngineId;
  private CustomSearchAPIService _searchService;
  
  
  public TermSearcher() 
  {
    _searchEngineId = Environment.GetEnvironmentVariable(SearchEngineIdVariable) ?? string.Empty;
    string googleApiKey = Environment.GetEnvironmentVariable(GoogleApiKeyVariable) ?? string.Empty;

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