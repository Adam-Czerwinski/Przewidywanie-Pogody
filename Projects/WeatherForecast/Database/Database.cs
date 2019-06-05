using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class Database
    {
        static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Metoda zwracający ostatni index występujący w danej tabeli
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns> Ostatni index </returns>
        public static int GetLastIndex(TableName tableName)
        {
            string GET_LAST_INDEX = "SELECT " + "`id_" + tableName.ToString() +  "` FROM `" + tableName.ToString() + "` order by 1 desc limit 1";

            int id = 0;

            using (MySqlCommand comm = new MySqlCommand(GET_LAST_INDEX, connection))
            {
                connection.Open();

                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                    id = int.Parse(reader.GetValue(0).ToString());

                reader.Close();
                connection.Close();
            }

            return id;
        }
    }
}
