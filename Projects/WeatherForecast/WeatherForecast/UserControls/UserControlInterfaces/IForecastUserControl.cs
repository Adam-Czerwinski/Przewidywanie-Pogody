using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.UserControls.UserControlInterfaces
{
    public interface IForecastUserControl
    {
        string[,,] ForecastDataOut { set; }
        string[] ForecastDataIn { get; }

        event Action ForecastAction;
    }
}
