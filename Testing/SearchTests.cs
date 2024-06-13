using WebSummary.InfoEngine;
using System.Collections;
using NUnit.Framework.Internal;
using System.Diagnostics;

namespace WebSummary.Testing;

public class SearchTests
{
  private TermSearcher _searcher;


  [SetUp]
  public void Setup()
  {
    IDictionary environmentVariables = Environment.GetEnvironmentVariables();

    if(!environmentVariables.Contains(TermSearcher.SearchEngineIdVariable)
       || !environmentVariables.Contains(TermSearcher.GoogleApiKeyVariable))
    {
      Assert.Fail();
    }

    _searcher = new TermSearcher();
  }

  [Test]
  public void SearchingForTerms()
  {
    var catResults = _searcher.GetResultsForTerm("cat", 5);
    var dogResults = _searcher.GetResultsForTerm("dog", 10);

    Assert.Multiple(() =>
    {
      Assert.That(catResults.Count, Is.EqualTo(5));
      Assert.That(dogResults.Count, Is.EqualTo(10));
    });
  }
}
