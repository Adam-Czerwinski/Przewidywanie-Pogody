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
            MySqlConnectionStringBuilder conStrBuilder = new MySqlConnectionStringBuilder();

            conStrBuilder.Server = "localhost";

            conStrBuilder.UserID = "root";

            conStrBuilder.Password = "";

            conStrBuilder.Database = "forecast_weather";

            Console.WriteLine(conStrBuilder.ToString());

            Connection = new MySqlConnection(conStrBuilder.ToString());
        }

    }
}
