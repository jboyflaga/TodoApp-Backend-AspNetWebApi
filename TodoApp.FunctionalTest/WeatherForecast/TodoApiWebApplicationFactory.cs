using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.WebApi;
using TodoApp.WebApi.Services;

namespace TodoApp.FunctionalTests;

/**
 * WRITING YOUR OWN WebApplicationFactory: https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api#writing-your-own-webapplicationfactory
 * by Tim Deschryver
 * 
 * In a real application, we have to deal with external dependencies, and these might need to be mocked. 
 * For example, to prevent that e-mails are sent as a side effect of a test.
 * 
 * But I do suggest keeping as many as possible real instances of dependencies that you're in control of, 
 * for example, the database. For the dependencies that are out of your reach, 
 * mostly 3rd-party driven-ports, there's a need to create mocked instances. 
 * This allows you to return expected data and prevents that test data is created in a 3rd party service.
 * 
 * The WebApplicationFactory allows you to alter the internals of the application, 
 * intervene with the pipeline of a request, or to replace objects in the Dependency Injection (DI) container.
 */

public class TodoApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Is to be called after the `ConfigureServices` from the Startup
        // which allows you to overwrite the DI with mocked instances
        builder.ConfigureTestServices(services =>
        {
            services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigStub>();
        });
    }
}