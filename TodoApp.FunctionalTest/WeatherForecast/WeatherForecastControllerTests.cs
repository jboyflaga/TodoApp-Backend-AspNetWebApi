using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TodoApp.WebApi;
using TodoApp.WebApi.Entities;
using TodoApp.WebApi.Models.WeatherForecast;

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

        var forecast = await client.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");
        forecast.Should().HaveCount(7);
    }
}