namespace TodoApp.WebApi.Services
{
    public interface IWeatherForecastConfigService
    {
        int NumberOfDays();
    }

    public class WeatherForecastConfigService : IWeatherForecastConfigService
    {
        public int NumberOfDays() => 7;
    }
}
