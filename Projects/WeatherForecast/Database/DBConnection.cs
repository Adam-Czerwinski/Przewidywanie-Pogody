using System;
using MySql.Data.MySqlClient;

namespace Database
{
    public class DBConnection
    {

        public MySqlConnection Connection { get; private set; }

        private static DBConnection instance = null;
        public static DBConnection Instance
        {
            get
            {
                return instance ?? (instance = new DBConnection());
            }
        }

        private DBConnection()
        {
            MySqlConnectionStringBuilder conStrBuilder = new MySqlConnectionStringBuilder
            {
                Server = "localhost",

                UserID = "root",

                Password = "root",

                Database = "forecast_weather"
            };

            Connection = new MySqlConnection(conStrBuilder.ToString());
        }

    }
}
