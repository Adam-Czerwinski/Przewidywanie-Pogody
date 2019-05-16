using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataInsert
{
    class InsertWeatherData
    {

        public static void Insert(List<WeatherData> datas)
        {
            MySqlConnection connection = DBConnection.Instance.Connection;

            string insertCommand;

            connection.Open();

            foreach (WeatherData d in datas)
            {
                try
                {
                    insertCommand = "INSERT INTO `weather_data` VALUES ( "
                    + d.IdWeatherData + ", "
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
                    Console.WriteLine("Error: " + e + "\n");
                }
            }

            connection.Close();
        }

    }
}
