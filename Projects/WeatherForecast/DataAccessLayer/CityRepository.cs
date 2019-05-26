using System;
using System.Collections.Generic;
using BusinessObject;
using MySql.Data.MySqlClient;
using Database;

namespace DataAccessLayer
{
    public class CityRepository
    {
        private static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Dodaj wiele miast do bazy danych
        /// </summary>
        /// <param name="cities">Lista miast</param>
        public static void Save(IReadOnlyList<City> cities)
        {
            string insertCommand;

            connection.Open();

            foreach (City c in cities)
            {
                try
                {
                    insertCommand = "INSERT INTO `cities` VALUES ( " + c.IdCity + ", \"" + c.Name + "\", \"" + c.Region + "\", " + c.IsStation + " )";

                    using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                    {
                        commamnd.ExecuteReader();
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
        public static void Save(City city)
        {
            string insertCommand;

            connection.Open();

                try
                {
                    insertCommand = "INSERT INTO `cities` VALUES ( " + city.IdCity + ", \"" + city.Name + "\", \"" + city.Region + "\", " + city.IsStation + " )";

                    using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                    {
                        commamnd.ExecuteReader();
                        //Console.WriteLine("Dodano: " + c.ToString());
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
