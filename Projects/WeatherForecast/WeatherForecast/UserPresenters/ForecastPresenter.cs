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

            string[,,] forecastWeatherData = new string[4, 3, 6];

            int indexDay = 0;
            int indexHour = 0;

            foreach (WeatherData[] day in weatherDatas)
            {
                indexHour = 0;
                foreach (WeatherData hour in day)
                {
                    if (hour != null)
                    {
                        forecastWeatherData[indexDay, indexHour, 0] = String.Format("{0:N1}", hour.Temperature);
                        forecastWeatherData[indexDay, indexHour, 1] = hour.Humidity.ToString();
                        if (hour.WindSpeed > 0)
                            forecastWeatherData[indexDay, indexHour, 2] = hour.WindSpeed.ToString();
                        else
                            forecastWeatherData[indexDay, indexHour, 2] = 0.ToString();
                        forecastWeatherData[indexDay, indexHour, 3] = hour.WindDirection.ToString();
                        forecastWeatherData[indexDay, indexHour, 4] = hour.Cloudy.ToString();
                        forecastWeatherData[indexDay, indexHour, 5] = hour.Visibility.ToString();
                    }
                    else
                    {
                        forecastWeatherData[indexDay, indexHour, 0] = "-";
                        forecastWeatherData[indexDay, indexHour, 1] = "-";
                        forecastWeatherData[indexDay, indexHour, 2] = "-";
                        forecastWeatherData[indexDay, indexHour, 3] = "-";
                        forecastWeatherData[indexDay, indexHour, 4] = "-";
                        forecastWeatherData[indexDay, indexHour, 5] = "-";
                    }
                    indexHour++;
                }
                indexDay++;
            }

            _forecastUserControl.ForecastDataOut = forecastWeatherData;
        }
    }
}
