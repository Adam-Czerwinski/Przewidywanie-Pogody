using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.UserControls.UserControlInterfaces
{
    public interface IForecastUserControl
    {
        string this[int indexer1, int indexer2, int indexer3] { set; }
        string[] ForecastDataIn { get; }

        event Action ForecastAction;
    }
}
