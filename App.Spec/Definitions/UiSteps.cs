using AwesomeAssertions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace Study.App.Spec.Definitions;

[Binding]
[Scope(Tag = "ui")]
public class UiSteps(IWebDriver webDriver, IServer server,
    IReqnrollOutputHelper outputHelper) {
    private readonly string _origin = server.Features
        .Get<IServerAddressesFeature>()?
        .Addresses
        .DefaultIfEmpty()
        .First() ?? throw new InvalidOperationException("no origin");

    [AfterScenario]
    public void TakeScreenshotOnFailure(ScenarioContext scenarioContext)
    {
        if (scenarioContext.TestError != null)
            webDriver.TakeScreenshotTo(outputHelper);
    }

    [When("client loads {string} in the browser")]
    public void WhenClientLoadsStringInTheBrowser(string uri)
    {
        webDriver.Navigate().GoToUrl($"{_origin}{uri}");
    }

    [Then("the page shows {string} headline")]
    public void ThenThePageShowsStringHeadline(string headline)
    {
        webDriver.Find("main h1").Text.Should().Be(headline);
    }
}

public static class Ext {
    public static IWait<T> Wait<T>(this T context)
    {
        return new DefaultWait<T>(context) {
            PollingInterval = TimeSpan.FromMilliseconds(100),
            Timeout = TimeSpan.FromSeconds(5)
        };
    }

    public static IWebElement Find(this ISearchContext context, string css)
    {
        return context.Wait().Until(c => c.FindElements(By.CssSelector(css))
            .Where(e => e.Displayed)
            .DefaultIfEmpty()
            .First());
    }

    public static void TakeScreenshotTo(this IWebDriver webDriver,
        IReqnrollOutputHelper outputHelper)
    {
        const string screenshot = "screenshot.png";

        webDriver.TakeScreenshot().SaveAsFile(screenshot);
        outputHelper.AddAttachment(screenshot);
        File.Delete(screenshot);
    }
}
