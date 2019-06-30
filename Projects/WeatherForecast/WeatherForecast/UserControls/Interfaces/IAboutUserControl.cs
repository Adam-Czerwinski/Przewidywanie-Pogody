using System;

namespace WeatherForecast.UserControls.UserControlInterfaces
{
    public interface IAboutUserControl
    {
        string Path_ { set; }

        event Action Load_;
    }
}
