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

public class WeatherForecastControllerTests
{
    [Fact]
    public async Task GET_retrieves_weather_forecast()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var response = await client.GetAsync("/weatherforecast");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GET_returns_correct_number_of_weather_forecasts()
    {
        await using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        var forecast = await client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");
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