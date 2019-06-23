using System;

namespace WeatherForecast.UserControls.UserControlInterfaces
{
    public interface IHistoryUserControl
    {
        string[][] forecastDataIn { set; }

        event Action Load_;

    }
}
