using System;
using System.Linq;
using NeuralNetwork;
using BusinessObject;
using DataAccessLayer;

namespace WeatherForecast
{
    class Model
    {
        private ForecastWeather forecast;
        public Model()
        {
            forecast = new ForecastWeather();
            forecast.SetupNetwork();
        }

        #region Forecast
        public WeatherData[][] ForecastData(string[] forecastDataIn)
        {
            try
            {
                City city = new City(CityRepository.getAll().Last().IdCity + 1, forecastDataIn[0], (Regions)Enum.Parse(typeof(Regions), forecastDataIn[1]), false);
                CityRepository.Add(city);

                WeatherData[][] weatherDatas;

                WeatherData d = new WeatherData(1, city.IdCity, DateTime.Now, GetHour(), double.Parse(forecastDataIn[2]), int.Parse(forecastDataIn[3]),
                     (WindDirections)Enum.Parse(typeof(WindDirections), forecastDataIn[4]), int.Parse(forecastDataIn[5]), int.Parse(forecastDataIn[6]),
                     int.Parse(forecastDataIn[7]), DataTypes.User_input_data);

                return forecast.ForecastNextThreeDays(d);
            }
            catch (Exception) { return null; }
        }

        private int GetHour()
        {
            int hour = DateTime.Now.Hour;

            if (hour <= 6)
            {
                return 6;
            }
            else if (hour <= 12)
            {
                return 12;
            }
            else
            {
                return 18;
            }
        }
        #endregion
    }
}
