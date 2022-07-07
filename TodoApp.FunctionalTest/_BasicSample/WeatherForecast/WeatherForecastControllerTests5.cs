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
 * Using A CUSTOM AND REUSABLE XUNIT FIXTURE: IntegrationTest
 * by Tim Deschryver
 * 
 * This makes it possible to filter a test run to only run the integration tests of your project, or to exclude them.
 * 
 * dotnet test --filter Category=Integration
 * dotnet test --filter Category!=Integration
 */
public class WeatherForecastControllerTests5 : IntegrationTest
{
    public WeatherForecastControllerTests5(TodoApiWebApplicationFactory fixture)
        : base(fixture) { }

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
        forecast.Should().HaveCount(WeatherForecastConfigStub.NUMBER_OF_DAYS);
    }

    [Fact]
    public async Task GET_with_invalid_config_results_in_a_bad_request()
    {
        var clientWithInvalidConfig = _factory.WithWebHostBuilder(builder =>
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