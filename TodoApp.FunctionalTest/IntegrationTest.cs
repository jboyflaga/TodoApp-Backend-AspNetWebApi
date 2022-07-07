using Microsoft.Extensions.Configuration;
using Respawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.FunctionalTests;

[Collection("Sequential")] // run xUnit sequentially, instead of in parallel
[Trait("Category", "Integration")]
public abstract class IntegrationTest : IClassFixture<TodoApiWebApplicationFactory>
{
    private readonly Checkpoint _checkpoint = new Checkpoint
    {
        //SchemasToInclude = new[] {
        //    "Playground"
        //},
        WithReseed = true
    };

    protected readonly TodoApiWebApplicationFactory _factory;
    protected readonly HttpClient _client;

    public IntegrationTest(TodoApiWebApplicationFactory fixture)
    {
        _factory = fixture;
        _client = _factory.CreateClient();

        // if needed, reset the DB
        _checkpoint.Reset(_factory.Configuration.GetConnectionString("WebApiDatabase")).Wait();
    }
}