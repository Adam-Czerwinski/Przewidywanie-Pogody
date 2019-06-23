using System;

namespace WeatherForecast.UserControls.UserControlInterfaces
{
    public interface IStatisticUserControl
    {
        StatisticData StatisticData { get; }
        string Year_ { get; }
        string Month_ { get; }

        event Action LoadYearly_;
        event Action LoadMonthly_;
        event Action LoadDaily_;
    }
}
