using System.Net;
using AwesomeAssertions;
using AwesomeAssertions.Json;
using Newtonsoft.Json.Linq;

namespace Study.App.Spec.Definitions;

[Binding]
public class WeatherSteps {
    private HttpResponseMessage _response = null!;

    [When("client calls GET {string}")]
    public async Task WhenClientCallsApiGet(string uri)
    {
        _response = await Global.Client.GetAsync(uri);
    }

    [Then("it produces {int} day weather forecast")]
    public async Task ThenItProducesWeatherForecast(int days)
    {
        _response.StatusCode.Should().Be(HttpStatusCode.OK);
        _response.Content.Headers.ContentType?.MediaType
            .Should().Be("application/json");

        var body = JToken.Parse(await _response.Content.ReadAsStringAsync());

        body.Should().HaveCount(days);
    }
}
