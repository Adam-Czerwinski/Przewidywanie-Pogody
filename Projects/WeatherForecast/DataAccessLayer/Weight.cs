using Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Weight
    {
        static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Metoda dodająca wagi do nazy. Parametr podać najlepiej za pomocą funkcji Program.PobierzWagi(...)
        /// </summary>
        /// <param name="value"></param>
        public static void AddWeight(string value)
        {
            string ADD_WEIGHT = "INSERT INTO `weight` VALUES ( null, '" + value + "')";

            using (MySqlCommand comm = new MySqlCommand(ADD_WEIGHT, connection))
            {
                connection.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                connection.Close();
            }
        }

    }
}
