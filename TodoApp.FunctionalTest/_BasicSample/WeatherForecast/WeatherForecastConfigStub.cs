using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.WebApi.Services;

namespace TodoApp.FunctionalTests;

public class WeatherForecastConfigStub : IWeatherForecastConfigService
{
    public const int NUMBER_OF_DAYS = 10;
    public int NumberOfDays() => NUMBER_OF_DAYS;
}