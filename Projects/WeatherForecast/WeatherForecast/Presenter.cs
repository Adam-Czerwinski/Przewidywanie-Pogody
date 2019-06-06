using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast
{
    class Presenter
    {
        private IView view;
        private Model model;

        public Presenter(IView view, Model model)
        {
            this.view = view;
            this.model = model;

            view.ForecastUserControl.ForecastAction += ForecastWeather;
        }

        private void ForecastWeather()
        {
            view.ForecastUserControl.ForecastData = model.ForecastData
            (
                view.ForecastUserControl.City, view.ForecastUserControl.RegionPL,
                view.ForecastUserControl.Temperature, view.ForecastUserControl.Humidity,
                view.ForecastUserControl.WindSpeed, view.ForecastUserControl.WindDirection,
                view.ForecastUserControl.Cloudy, view.ForecastUserControl.Visibility
            );
        }

    }
}
