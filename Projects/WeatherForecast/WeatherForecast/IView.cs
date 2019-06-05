using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.UserControls;

namespace WeatherForecast
{
    interface IView
    {
        ForecastUserControl ForecastUserControl { get; }
        HistoryUserControl HistoryUserControl { get; }
        StatisticUserControl StatisticUserControl { get; }
        NeuralNetUserControl NeuralNetUserControl { get; }
        AboutUserControl AboutUserControl { get; }
    }
}
