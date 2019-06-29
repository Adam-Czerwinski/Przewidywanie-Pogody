﻿using System;
using System.Collections.Generic;
using BusinessObject;
using Database;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class CityRepository
    {
        private static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Dodaj wiele miast do bazy danych
        /// </summary>
        /// <param name="cities">Lista miast</param>
        public static void Add(IReadOnlyList<City> cities)
        {
            string insertCommand;
            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("Can't connect with database!");
                return;
            }


            foreach (City c in cities)
            {
                try
                {
                    insertCommand = "INSERT INTO `cities` VALUES ( " + "null" + ", \"" + c.Name + "\", \"" + c.Region + "\", " + c.IsStation + " )";

                    using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                    {
                        commamnd.ExecuteNonQuery();
                        //Console.WriteLine("Dodano: " + c.ToString());
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
        /// <param name="city">miasto</param>
        private static void Add(City city)
        {
            string insertCommand;
            try
            {
                connection.Open();
                insertCommand = "INSERT INTO `cities` VALUES ( " + "null" + ", \"" + city.Name + "\", \"" + city.Region + "\", " + city.IsStation + " )";

                using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                {
                    commamnd.ExecuteNonQuery();
                    //Console.WriteLine("Dodano: " + city);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message + "\n");
            }

            connection.Close();
        }

        /// <summary>
        /// Zwraca id miasta w bazie. Jeśli w bazie nie ma jeszcze danego miasta to je dodaje i zwraca jego id.
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static int GetIdCity(City city)
        {
            MySqlConnection connection = DBConnection.Instance.Connection;

            string GET_USER = "select id_cities from cities where name = '" + city.Name + "' and region = '" + city.Region + "';";

            int id = -1;

            try
            {
                using (MySqlCommand comm = new MySqlCommand(GET_USER, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = comm.ExecuteReader();
                    if (reader.Read())
                        id = int.Parse(reader.GetValue(0).ToString());

                    reader.Close();
                    connection.Close();

                    if (id == -1)
                    {
                        Add(city);
                        id = GetIdCity(city);
                    }

                    return id;
                }
            }
            catch (Exception) { return id; }
        }

        public static List<City> getAll()
        {
            string selectcommand = "SELECT `Id_cities`, `Name`, `Region`, `Is_station` FROM Cities;";
            List<City> cities = new List<City>();

            int idCity;
            string name;
            Regions region;
            bool isStation;

            try
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(selectcommand, connection))
                {
                    MySqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        idCity = Convert.ToInt32(dataReader["Id_cities"]);
                        name = dataReader["Name"].ToString();
                        string regionName = dataReader["Region"].ToString();
                        region = (Regions)Enum.Parse(typeof(Regions), regionName);
                        isStation = (bool)dataReader["Is_station"];
                        cities.Add(new City(idCity, name, region, isStation));
                    }
                    dataReader.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message + "\n");
            }


            connection.Close();

            return cities;
        }
    }
}
