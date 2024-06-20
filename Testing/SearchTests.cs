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

    Assert.Multiple(() =>
    {
      Assert.That(environmentVariables.Contains(TermSearcher.SearchEngineIdVariable), Is.True, "Search engine id missing!");
      Assert.That(environmentVariables.Contains(TermSearcher.GoogleApiKeyVariable), Is.True, "Google API key missing!");
    });

    _searcher = new TermSearcher();
  }

  [Test]
  public void SearchingForTerms()
  {
    var catResults = _searcher.GetResultsForTerm("cat");
    var dogResults = _searcher.GetResultsForTerm("dog", 10);

    Assert.Multiple(() =>
    {
      Assert.That(catResults.Count, Is.EqualTo(5));
      Assert.That(dogResults.Count, Is.EqualTo(10));
    });
  }
}
