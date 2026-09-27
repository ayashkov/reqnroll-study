using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Testing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.BoDi;

namespace Study.App.Spec.Definitions;

[Binding]
public class Setup {
    [BeforeTestRun]
    public static void BeforeRun(IObjectContainer container)
    {
        container.RegisterTypeAs<TestApplicationFactory<Program>,
            WebApplicationFactory<Program>>();
        container.RegisterFactoryAs(c =>
            c.Resolve<WebApplicationFactory<Program>>().CreateClient());
        container.RegisterTypeAs<TestWebDriver, IWebDriver>();
    }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class TestApplicationFactory<P> : WebApplicationFactory<P>
    where P : class {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class TestWebDriver : ChromeDriver;
