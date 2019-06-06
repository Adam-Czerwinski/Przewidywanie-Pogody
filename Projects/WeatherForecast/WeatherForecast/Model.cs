using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public string[,,] ForecastData
        (
            string cityName, string region, double temperature, int humidity,
            int windSpeed, string windDirection, int cloudy, int visibility
        )
        {
            City city = new City(CityRepository.getAll().Last().IdCity + 1, cityName, (Regions)Enum.Parse(typeof(Regions), region), false);
            CityRepository.Add(city);

            WeatherData[][] weatherDatas;

            weatherDatas = forecast.ForecastNextThreeDays(new WeatherData(1, city.IdCity, DateTime.Now, GetHour(), temperature, humidity,
                (WindDirections)Enum.Parse(typeof(WindDirections), windDirection), windSpeed, cloudy, visibility, DataTypes.User_input_data));

            string[,,] forecastWeatherData = new string[4, 3, 6];

            int indexDay = 0;
            int indexHour = 0;

            foreach(WeatherData[] day in weatherDatas)
            {
                indexHour = 0;
                foreach(WeatherData hour in day)
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
                    indexHour++;
                }
                indexDay++;
            }

            return forecastWeatherData;
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
    }
}
