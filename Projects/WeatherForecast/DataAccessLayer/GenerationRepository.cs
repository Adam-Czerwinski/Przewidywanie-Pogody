using Database;
using MySql.Data.MySqlClient;


namespace DataAccessLayer
{
    public class GenerationRepository
    {
        static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Metoda dodająca konfigurację sieci do bazy 
        /// </summary>
        /// <param name="neurons_in"></param>
        /// <param name="neurons_hidden"></param>
        /// <param name="neurons_out"></param>
        /// <param name="learning_rate"></param>
        /// <param name="id_activation_function"></param>
        public static void Add( int neurons_in, int neurons_hidden, int neurons_out, double learning_rate, int id_activation_function)
        {
            string ADD_GENERATION = "INSERT INTO `generations` VALUES ( null, " + 
                                            + neurons_in + ", " 
                                            + neurons_hidden + ", "
                                            + neurons_out + ", " 
                                            + learning_rate.ToString().Replace(',','.') + ", "
                                            + id_activation_function + ")";

            using (MySqlCommand comm = new MySqlCommand(ADD_GENERATION, connection))
            {
                connection.Open();

                comm.ExecuteNonQuery();

                connection.Close();
            }
        }

        /// <summary>
        /// Metoda dodająca konfigurację sieci do bazy Uwaga!!! Podaj poprawna nazwe funkcji, jelsi podasz zla nazwe to zostanei ona dodana do bazy jako nowa 
        /// funkcja aktywacji!!!
        /// </summary>
        /// <param name="neurons_in"></param>
        /// <param name="neurons_hidden"></param>
        /// <param name="neurons_out"></param>
        /// <param name="learning_rate"></param>
        /// <param name="name_activation_function"></param>
        public static void Add(int neurons_in, int neurons_hidden, int neurons_out, double learning_rate, string name_activation_function)
        {   
            string GET_ACTIVATION_FUNC = "SELECT " + "`id_activation_functions` FROM `activation_functions` WHERE name_activation_functions = '" 
                                     + name_activation_function + "'";

            // Sprawdzanie czy dana funkcja aktywacji istnieje w bazie
            int id = 0;

            using (MySqlCommand comm = new MySqlCommand(GET_ACTIVATION_FUNC, connection))
            {
                connection.Open();

                MySqlDataReader reader = comm.ExecuteReader();
                if (reader.Read())
                    id = int.Parse(reader.GetValue(0).ToString());

                connection.Close();
            }

            if(id != 0)
            {
                // Jeśli funkcja aktywacyjan jest w bazie to dodaje generacje 
                string ADD_GENERATION = "INSERT INTO `generations` VALUES ( null, " +
                                             +neurons_in + ", "
                                             + neurons_hidden + ", "
                                             + neurons_out + ", "
                                             + learning_rate.ToString().Replace(',', '.') + ", "
                                             + id + ")";

                using (MySqlCommand comm = new MySqlCommand(ADD_GENERATION, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = comm.ExecuteReader();

                    connection.Close();
                }

            }
            else
            {
                // Jeślni nie ma funkcji aktywacji w bazie to ją dodaje a następnie dodaje generacje 
                string ADD_ACTIVATION_FUNC = "INSERT INTO `activation_functions` VALUES (null, '" + name_activation_function + "')";

                using (MySqlCommand comm = new MySqlCommand(ADD_ACTIVATION_FUNC, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = comm.ExecuteReader();

                    connection.Close();
                }

                string ADD_GENERATION = "INSERT INTO `generations` VALUES ( null, " +
                                             +neurons_in + ", "
                                             + neurons_hidden + ", "
                                             + neurons_out + ", "
                                             + learning_rate.ToString().Replace(',', '.') + ", "
                                             + Database.Database.GetLastIndex(TableName.activation_functions) + ")";

                using (MySqlCommand comm = new MySqlCommand(ADD_GENERATION, connection))
                {
                    connection.Open();

                    MySqlDataReader reader = comm.ExecuteReader();

                    connection.Close();
                }

            }
        }

    }
}
