using Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class LearningProcess
    {

        static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Metoda wprowadzajaca learning process do bazy Uwaga!!! proces dotyczy ostatniej dodanej generacji i ostatnio dodanych wag!!!
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="epoch"></param>
        /// <param name="total_error"></param>
        /// <param name="is_learned"></param>
        public static void AddLearningProcess(uint iteration, int epoch, double total_error, bool is_learned)
        {
            string ADD_LEARNING_PROCESS = "INSERT INTO `learning_process` VALUES ( null, "
                                          + Database.Database.GetLastIndex(TableName.generations) + ", "
                                          + Database.Database.GetLastIndex(TableName.weight) + ", "
                                          + iteration + ", "
                                          + epoch + ", "
                                          + total_error.ToString().Replace(',', '.') + ", "
                                          + is_learned + ")";

            using (MySqlCommand comm = new MySqlCommand(ADD_LEARNING_PROCESS, connection))
            {
                connection.Open();

                MySqlDataReader reader = comm.ExecuteReader();

                connection.Close();
            }
        }


    }
}
