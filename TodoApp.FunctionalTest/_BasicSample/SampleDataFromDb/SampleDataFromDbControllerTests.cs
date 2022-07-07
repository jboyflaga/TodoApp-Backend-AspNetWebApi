using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
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
        var fixture = new Fixture();
        var data1 = fixture.Create<SampleDataFromDb>();
        data1.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data1);

        var data2 = fixture.Create<SampleDataFromDb>();
        data2.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data2);


        var response = await _client.GetAsync("api/SampleDataFromDb");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var response2 = await _client.GetFromJsonAsync<SampleDataFromDb[]>("api/SampleDataFromDb");
        response2.Should().HaveCount(2);
    }

    [Fact]
    public async Task GET_retrieves_correct_record()
    {
        var fixture = new Fixture();
        var data1 = fixture.Create<SampleDataFromDb>();
        data1.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data1);

        var data2 = fixture.Create<SampleDataFromDb>();
        data2.Id = 0; ; // fix for error "Cannot insert explicit value for identity column in table 'SampleDataFromDb' when IDENTITY_INSERT is set to OFF."
        await _client.PostAsJsonAsync("api/SampleDataFromDb", data2);


        var response = await _client.GetAsync("api/SampleDataFromDb/1");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var response2 = await _client.GetFromJsonAsync<SampleDataFromDb>("api/SampleDataFromDb/1");
        response2?.Id.Should().Be(1);
    }


    [Fact]
    public async Task GET_with_invalid_id_results_in_not_found()
    {
        var response = await _client.GetAsync("api/SampleDataFromDb/12345");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}