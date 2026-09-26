using Microsoft.AspNetCore.Mvc.Testing;

namespace Study.App.Spec.Definitions;

[Binding]
public class Global
{
    private static TestApplicationFactory<Program>? _appFactory;

    [BeforeTestRun]
    public static void BeforeRun()
    {
        _appFactory = new TestApplicationFactory<Program>();
    }

    [AfterTestRun]
    public static void AfterRun()
    {
        if (_appFactory is not null) {
            _appFactory.Dispose();
            _appFactory = null;
        }
    }

    public static HttpClient Client => _appFactory is null
        ? throw new InvalidOperationException("not initialized")
        : _appFactory.CreateClient();
}

internal class TestApplicationFactory<TProgram> :
    WebApplicationFactory<TProgram> where TProgram : class {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
    }
}
