using Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using BusinessObject;

namespace DataAccessLayer
{
    public class StatisticRepository
    {
        static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Zwraca statystyki temperaturowe dla danego okresu
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string[][][] Get(StatisticType type, string year, string month)
        {
            string GET_MIN = "select year(date_), min(temperature) from weather_data group by year(date_);";
            string GET_MID = "select year(date_), avg(temperature) from weather_data group by year(date_);";
            string GET_MAX = "select year(date_), max(temperature) from weather_data group by year(date_);";

            if (type == StatisticType.Daily)
            {
                GET_MIN = "select day(date_), min(temperature) from weather_data where year(date_) = '" + year + "' and month(date_) = '" + month + "' group by 1;";
                GET_MID = "select day(date_), avg(temperature) from weather_data where year(date_) = '" + year + "' and month(date_) = '" + month + "' group by 1;";
                GET_MAX = "select day(date_), max(temperature) from weather_data where year(date_) = '" + year + "' and month(date_) = '" + month + "' group by 1;";
            }
            if (type == StatisticType.Monthly)
            {
                GET_MIN = "select month(date_), min(temperature) from weather_data where year(date_) = '" + year + "' group by month(date_);";
                GET_MID = "select month(date_), avg(temperature) from weather_data where year(date_) = '" + year + "' group by month(date_);";
                GET_MAX = "select month(date_), max(temperature) from weather_data where year(date_) = '" + year + "' group by month(date_);";
            }

            List<string> _tempList = new List<string>();
            List<string> _nameList = new List<string>();
            string[][][] data = new string[3][][];

            for (int i = 0; i < 3; i++)
                data[i] = new string[2][];

            try
            {
                using (MySqlCommand comm = new MySqlCommand(GET_MIN, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        _tempList.Add(reader.GetValue(1).ToString());
                        _nameList.Add(reader.GetValue(0).ToString());
                    }
                    data[0][0] = _nameList.ToArray();
                    data[0][1] = _tempList.ToArray();

                    _tempList.Clear();
                    _nameList.Clear();

                    connection.Close();
                }
            }
            catch (Exception) { }

            try
            {
                using (MySqlCommand comm = new MySqlCommand(GET_MID, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        _tempList.Add(reader.GetValue(1).ToString());
                        _nameList.Add(reader.GetValue(0).ToString());
                    }
                    data[1][0] = _nameList.ToArray();
                    data[1][1] = _tempList.ToArray();

                    _tempList.Clear();
                    _nameList.Clear();

                    connection.Close();
                }
            }
            catch (Exception) { }

            try
            {
                using (MySqlCommand comm = new MySqlCommand(GET_MAX, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        _tempList.Add(reader.GetValue(1).ToString());
                        _nameList.Add(reader.GetValue(0).ToString());
                    }
                    data[2][0] = _nameList.ToArray();
                    data[2][1] = _tempList.ToArray();

                    _tempList.Clear();
                    _nameList.Clear();

                    connection.Close();
                }
            }
            catch (Exception) { }

            return data;
        }

    }
}
