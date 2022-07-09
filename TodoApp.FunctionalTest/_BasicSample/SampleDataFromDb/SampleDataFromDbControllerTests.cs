using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TodoApp.WebApi;
using TodoApp.WebApi.Entities;
using TodoApp.WebApi.Models.WeatherForecast;

namespace TodoApp.FunctionalTests;

public class SampleDataFromDbControllerTests : IntegrationTest
{
    public SampleDataFromDbControllerTests(TodoApiWebApplicationFactory fixture)
        : base(fixture) { }

    [Theory, AutoData]
    public async Task POST_creates_new_record(SampleDataFromDb sampleDataFromDb)
    {
        sampleDataFromDb.Id = 0; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        var response = await _client.PostAsJsonAsync("api/SampleDataFromDb", sampleDataFromDb);
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task GET_all_retrieves_records()
    {
        // arrange
        var fixture = new Fixture();
        var data1 = fixture.Create<SampleDataFromDb>();
        data1.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data1);

        var data2 = fixture.Create<SampleDataFromDb>();
        data2.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data2);


        // act
        var response = await _client.GetAsync("api/SampleDataFromDb");

        // assert
        string responseData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.Converters.Add(new JsonStringEnumConverter());
        var data = JsonSerializer.Deserialize<SampleDataFromDb[]>(responseData, options);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(2);
    }

    [Fact]
    public async Task GET_retrieves_correct_record()
    {
        // arrange
        var fixture = new Fixture();
        var data1 = fixture.Create<SampleDataFromDb>();
        data1.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data1);

        var data2 = fixture.Create<SampleDataFromDb>();
        data2.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data2);

        // act
        var response = await _client.GetAsync("api/SampleDataFromDb/1");

        // assert
        string responseData = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions();
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.Converters.Add(new JsonStringEnumConverter());
        var data = JsonSerializer.Deserialize<SampleDataFromDb>(responseData, options);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        data?.Id.Should().Be(1);
    }


    [Fact]
    public async Task GET_with_invalid_id_results_in_not_found()
    {
        var response = await _client.GetAsync("api/SampleDataFromDb/12345");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}