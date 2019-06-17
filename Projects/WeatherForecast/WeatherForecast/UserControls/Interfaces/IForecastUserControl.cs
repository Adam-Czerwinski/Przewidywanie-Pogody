using System;

namespace WeatherForecast.UserControls.UserControlInterfaces
{
    public interface IForecastUserControl
    {
        ForecastData ForecastData { get; }
        string[] ForecastDataIn { get; }
        event Action ForecastAction;
    }
}
