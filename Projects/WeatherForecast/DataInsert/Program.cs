using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInsert
{
    class Program
    {
        static void Main(string[] args)
        {
            DBConnection dbConnection = DBConnection.Instance;

            ReadDataFile datas = new ReadDataFile();

            foreach(City c in datas.Cities)
                Console.WriteLine(c.ToString());

            InsertCities.Insert(datas.Cities);

            InsertWeatherData.Insert(datas.WeatherDatas);

            Console.ReadKey();
        }
    }
}
