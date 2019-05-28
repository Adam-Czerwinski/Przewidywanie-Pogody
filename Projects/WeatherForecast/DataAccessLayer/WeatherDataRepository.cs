using System;
using System.Collections.Generic;
using BusinessObject;
using Database;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class WeatherDataRepository
    {
        private static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Dodaj wiele miast do bazy danych
        /// </summary>
        /// <param name="datas"></param>
        public static void Save(IReadOnlyList<WeatherData> datas)
        {
            string insertCommand;

            connection.Open();

            foreach (WeatherData d in datas)
            {
                try
                {
                    insertCommand = "INSERT INTO `weather_data` VALUES ( "
                    + "null" + ", "
                    + d.CityId + ", \""
                    + d.Date.Year + "." + d.Date.Month + "." + d.Date.Day + "\", \""
                    + d.Hour + ":00:00\", "
                    + d.Temperature.ToString().Replace(",", ".") + ", "
                    + d.Humidity + ", \""
                    + d.WindDirection + "\", "
                    + d.WindSpeed + ", "
                    + d.Cloudy + ", "
                    + d.Visibility + ", \""
                    + d.DataType + "\" )";

                    using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                    {
                        commamnd.ExecuteReader();
                        Console.WriteLine("Dodano: " + d.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message + "\n");
                }
            }

            connection.Close();
        }

        /// <summary>
        /// Dodaj jedno miasto do bazy danych
        /// </summary>
        /// <param name="data"></param>
        public static void Save(WeatherData data)
        {
            string insertCommand;

            connection.Open();

                try
                {
                    insertCommand = "INSERT INTO `weather_data` VALUES ( "
                    + "null" + ", "
                    + data.CityId + ", \""
                    + data.Date.Year + "." + data.Date.Month + "." + data.Date.Day + "\", \""
                    + data.Hour + ":00:00\", "
                    + data.Temperature.ToString().Replace(",", ".") + ", "
                    + data.Humidity + ", \""
                    + data.WindDirection + "\", "
                    + data.WindSpeed + ", "
                    + data.Cloudy + ", "
                    + data.Visibility + ", \""
                    + data.DataType + "\" )";

                    using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                    {
                        commamnd.ExecuteReader();
                    //Console.WriteLine("Dodano: " + data.ToString());
                }
            }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message + "\n");
                }
            

            connection.Close();
        }
    }
}
