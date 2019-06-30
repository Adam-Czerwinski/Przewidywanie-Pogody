using System;

namespace WeatherForecast.UserControls.UserControlInterfaces
{
    public interface INeuralNetUserControl
    {
        string Path_ { set; }

        event Action Load_;
    }
}
