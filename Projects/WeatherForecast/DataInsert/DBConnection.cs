using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataInsert
{
    class DBConnection
    {

        private static DBConnection instance = null;
        public MySqlConnection Connection { get; private set; }

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
