using MySql.Data.MySqlClient;
using System;
using Database;


namespace DataAccessLayer
{
    public class ActivationFunctionRepository
    {
        private static MySqlConnection connection = DBConnection.Instance.Connection;

        /// <summary>
        /// Dodaj wiele funkcji aktywacyjnych do bazy danych
        /// </summary>
        /// <param name="activationFunctions"></param>
        public static void Add(string[] activationFunctions)
        {
            string insertCommand;

            connection.Open();

            foreach (var s in activationFunctions)
            {
                try
                {
                    insertCommand = "INSERT INTO `activation_functions` VALUES ( "
                        + "null, \""
                        + s + "\");";

                    using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                    {
                        commamnd.ExecuteReader();
                        //Console.WriteLine("Dodano: " + s);
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
        /// Dodaj jedna funkcje aktywacyjna do bazy danych
        /// </summary>
        /// <param name="activationFunction"></param>
        public static void Add(string activationFunction)
        {
            string insertCommand;

            connection.Open();

            try
            {
                insertCommand = "INSERT INTO `activation_functions` VALUES ( "
                    + "null, \""
                    + activationFunction + "\");";

                using (MySqlCommand commamnd = new MySqlCommand(insertCommand, connection))
                {
                    commamnd.ExecuteReader();
                    Console.WriteLine("Dodano: " + activationFunction);
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
