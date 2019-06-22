using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetwork;
using BusinessObject;
using DataAccessLayer;
using Database;
using MySql.Data.MySqlClient;

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

                // Podczas tworzenia obiektu data, do bazy jest dodawane miasto (jeśli nie istnieje) 
                WeatherData data = new WeatherData(1, CityRepository.GetIdCity(city), DateTime.Now, GetHour(), double.Parse(forecastDataIn[2]), int.Parse(forecastDataIn[3]),
                         (WindDirections)Enum.Parse(typeof(WindDirections), forecastDataIn[4]), int.Parse(forecastDataIn[5]), int.Parse(forecastDataIn[6]),
                         int.Parse(forecastDataIn[7]), DataTypes.User_input_data);

                // Dodawanie user input do bazy
                WeatherDataRepository.Add(data);

                return data;
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
        #region History
        public string[][] LoadData()
        {
            MySqlConnection connection = DBConnection.Instance.Connection;
            string GET_DATA = "select id_weather_data, cities.name, date_, hour_, temperature, humidity,  wind_direction, wind_speed, cludy, visibility" +
                " from weather_data inner join cities on city = id_cities " +
                "where data_type = 'user_input_data'; ";
          
          
            List<string[]> userDatas = new List<string[]>();
            string[] uData = new string[10];
            try
            {
                using (MySqlCommand comm = new MySqlCommand(GET_DATA, connection))
                {
                    connection.Open();
                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            string el = uData[i];
                            uData[i] = reader.GetValue(i).ToString();
                        }
                        userDatas.Add(new string[10]{uData[0], uData[1],
                            uData[2], uData[3], uData[4], uData[5],
                            uData[6], uData[7], uData[8], uData[9]});
                    }
                    reader.Close();
                    connection.Close();
                    return userDatas.ToArray();
                }

            }
            catch (Exception) { return userDatas.ToArray(); }
        }
        #endregion
    }
}
