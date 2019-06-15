using BusinessObject;
using System;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserPresenters
{
    class ForecastPresenter
    {
        private IForecastUserControl _forecastUserControl;
        private Model _model;

        public ForecastPresenter(IForecastUserControl forecastUserControl, Model model)
        {
            _forecastUserControl = forecastUserControl;
            _model = model;

            _forecastUserControl.ForecastAction += _forecastAction;
        }

        private void _forecastAction()
        {

            WeatherData[][] weatherDatas = _model.ForecastData(_forecastUserControl.ForecastDataIn);

            int indexDay = 0;
            int indexHour = 0;

            foreach (WeatherData[] day in weatherDatas)
            {
                indexHour = 0;
                foreach (WeatherData hour in day)
                {
                    if (hour != null)
                    {
                        _forecastUserControl[indexDay, indexHour, 0] = String.Format("{0:N1}", hour.Temperature);
                        _forecastUserControl[indexDay, indexHour, 1] = hour.Humidity.ToString();
                        if (hour.WindSpeed > 0)
                            _forecastUserControl[indexDay, indexHour, 2] = hour.WindSpeed.ToString();
                        else
                            _forecastUserControl[indexDay, indexHour, 2] = 0.ToString();
                        _forecastUserControl[indexDay, indexHour, 3] = hour.WindDirection.ToString();
                        _forecastUserControl[indexDay, indexHour, 4] = hour.Cloudy.ToString();
                        _forecastUserControl[indexDay, indexHour, 5] = hour.Visibility.ToString();
                    }
                    else
                    {
                        _forecastUserControl[indexDay, indexHour, 0] = "-";
                        _forecastUserControl[indexDay, indexHour, 1] = "-";
                        _forecastUserControl[indexDay, indexHour, 2] = "-";
                        _forecastUserControl[indexDay, indexHour, 3] = "-";
                        _forecastUserControl[indexDay, indexHour, 4] = "-";
                        _forecastUserControl[indexDay, indexHour, 5] = "-";
                    }
                    indexHour++;
                }
                indexDay++;
            }

        }
    }
}
