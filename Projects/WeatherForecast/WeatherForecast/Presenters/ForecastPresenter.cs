using BusinessObject;
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
        
        /// <summary>
        /// Przewiduje pogodę i uzupełnia widok o przewidzianą pogodę
        /// </summary>
        private void _forecastAction()
        {
            WeatherData weather = _model.CreateWeatherData(_forecastUserControl.ForecastDataIn);
            WeatherData[][] weatherDatas = _model.ForecastData(weather);

            int indexDay = 0;
            int indexHour = 0;

            foreach (WeatherData[] day in weatherDatas)
            {
                indexHour = 0;
                foreach (WeatherData hour in day)
                {
                    if (hour != null)
                    {
                        _forecastUserControl.ForecastData[indexDay, indexHour, 0] = string.Format("{0:N1}", hour.Temperature);
                        _forecastUserControl.ForecastData[indexDay, indexHour, 1] = hour.Humidity.ToString();

                        if (hour.WindSpeed > 0)
                            _forecastUserControl.ForecastData[indexDay, indexHour, 2] = hour.WindSpeed.ToString();
                        else
                            _forecastUserControl.ForecastData[indexDay, indexHour, 2] = 0.ToString();

                        _forecastUserControl.ForecastData[indexDay, indexHour, 3] = hour.WindDirection.ToString();
                        _forecastUserControl.ForecastData[indexDay, indexHour, 4] = hour.Cloudy.ToString();
                        _forecastUserControl.ForecastData[indexDay, indexHour, 5] = hour.Visibility.ToString();
                    }
                    else
                        //i<6 bo 5 labelek do uzupełnienia
                        for(int i=0;i<6;i++)
                            _forecastUserControl.ForecastData[indexDay, indexHour, i] = "-";

                    indexHour++;
                }
                indexDay++;
            }

        }
    }
}
