using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Study.App.Spec.Definitions;

[Binding]
[Scope(Tag = "ui")]
public class UiSteps(IWebDriver webDriver, IServer server) {
    private readonly string _origin = server.Features
        .Get<IServerAddressesFeature>()?
        .Addresses
        .DefaultIfEmpty()
        .First() ?? throw new InvalidOperationException("no origin");

    [Given("the search page is loaded")]
    public void GivenTheSearchPageIsLoaded()
    {
        Console.WriteLine($">>> {_origin}");

        webDriver.Navigate().GoToUrl("https://duckduckgo.com/");
    }

    [When("client performs search for {string}")]
    public void WhenClientPerformsSearchForString(string term)
    {
        var input = webDriver.Find("textarea[name=q]");

        input.SendKeys(term);
        input.SendKeys(Keys.Enter);
    }

    [Then("the search produces results")]
    public void ThenTheSearchProducesResults()
    {
        webDriver.Find("ol li article");
    }
}

public static class Ext {
    public static IWait<T> Wait<T>(this T context)
    {
        return new DefaultWait<T>(context) {
            PollingInterval = TimeSpan.FromMilliseconds(100),
            Timeout = TimeSpan.FromSeconds(10)
        };
    }

    public static IWebElement Find(this ISearchContext context, string css)
    {
        return context.Wait().Until(c => c.FindElements(By.CssSelector(css))
            .Where(e => e.Displayed)
            .DefaultIfEmpty()
            .First());
    }
}
