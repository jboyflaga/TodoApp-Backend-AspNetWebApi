using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.WebApi.Services;

namespace TodoApp.FunctionalTests
{
    internal class InvalidWeatherForecastConfigStub : IWeatherForecastConfigService
    {
        public int NumberOfDays() => -3;
    }
}
