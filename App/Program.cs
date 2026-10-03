using System.Reflection;
using Microsoft.Extensions.FileProviders;

const string uiLocation = "Study.Ui";
var ui = new StaticFileOptions {
    FileProvider = new EmbeddedFileProvider(
        Assembly.Load(new AssemblyName(uiLocation)), uiLocation)
};
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseStaticFiles(ui);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

var summaries = new[] {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy",
    "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () => Enumerable
        .Range(1, 5)
        .Select(index => new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]))
        .ToArray()
    )
    .WithName("GetWeatherForecast");

app.MapFallbackToFile("index.html", ui);
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
