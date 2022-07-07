using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using TodoApp.WebApi;
using TodoApp.WebApi.Entities;
using TodoApp.WebApi.Models.WeatherForecast;
using TodoApp.WebApi.Services;

namespace TodoApp.FunctionalTests;

/**
 * A SIMPLE TEST USING XUNIT'S FIXTURES: https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api#a-simple-test
 * by Tim Deschryver
 * 
 * Because the test class implements the xUnit IClassFixture interface, the
 * test cases inside the class share a single test context. 
 * Meaning that the application is now only bootstrapped once for all the test cases 
 * inside the test class, and is disposed of when all tests have been completed.
 */
public class WeatherForecastControllerTests2 : IClassFixture<WebApplicationFactory<Program>>
{
    readonly HttpClient _client;

    public WeatherForecastControllerTests2(WebApplicationFactory<Program> application)
    {
        _client = application.CreateClient();
    }

    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        var response = await _client.GetAsync("/weatherforecast");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_returns_correct_number_of_weather_forecasts()
    {
        var forecast = await _client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");
        forecast.Should().HaveCount(7);
    }

    [Fact]
    public async Task GET_with_invalid_config_results_in_a_bad_request()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var clientWithInvalidConfig = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<IWeatherForecastConfigService, InvalidWeatherForecastConfigStub>();
            });
        })
        .CreateClient();

        var response = await clientWithInvalidConfig.GetAsync("/weatherforecast");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}