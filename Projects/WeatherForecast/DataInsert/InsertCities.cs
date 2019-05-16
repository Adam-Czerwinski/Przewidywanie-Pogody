using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataInsert
{
    class InsertCities
    {   

        public static void Insert(List<City> cities)
        {
            MySqlConnection connection = DBConnection.Instance.Connection;

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
                        Console.WriteLine("Dodano: " + c.ToString());
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
