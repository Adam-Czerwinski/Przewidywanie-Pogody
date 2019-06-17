﻿using System;
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

        /// <summary>
        /// Tworzy instancję WeatherData i jąz wraca
        /// </summary>
        /// <param name="forecastDataIn">Wprowadzone dane użytkownika</param>
        /// <returns>nowa instancja WeatherData</returns>
        public WeatherData CreateWeatherData(string[] forecastDataIn)
        {
            try
            {
                City city = new City(CityRepository.getAll().Last().IdCity + 1, forecastDataIn[0], (Regions)Enum.Parse(typeof(Regions), forecastDataIn[1]), false);
                CityRepository.Add(city);

                return new WeatherData(1, city.IdCity, DateTime.Now, GetHour(), double.Parse(forecastDataIn[2]), int.Parse(forecastDataIn[3]),
                         (WindDirections)Enum.Parse(typeof(WindDirections), forecastDataIn[4]), int.Parse(forecastDataIn[5]), int.Parse(forecastDataIn[6]),
                         int.Parse(forecastDataIn[7]), DataTypes.User_input_data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        #region Forecast
        public WeatherData[][] ForecastData(WeatherData wd)
        {
            return forecast.ForecastNextThreeDays(wd);
        }

        /// <summary>
        /// Zwraca godzinę zaokrąglając do góry
        /// </summary>
        /// <returns></returns>
        private int GetHour()
        {
            int hour = DateTime.Now.Hour;

            if (hour <= 6)
                return 6;
            else if (hour <= 12)
                return 12;
            else
                return 18;
        }
        #endregion
    }
}
