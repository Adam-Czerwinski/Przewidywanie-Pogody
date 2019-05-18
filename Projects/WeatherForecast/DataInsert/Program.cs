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

            //Console.Write("Ilość miast: " + datas.Cities.Count);
            //Console.ReadKey();

            //foreach (City c in datas.Cities)
            //    Console.WriteLine(c.ToString());


            //Console.Write("Ilość danych pogodowych: " + datas.WeatherDatas.Count);
            //Console.ReadKey();

            //foreach (var d in datas.WeatherDatas)
            //    Console.WriteLine(d.ToString());

            Console.WriteLine("Trwa dodawanie...");

            //wrzucanie danych do bazy
            InsertCities.Insert(datas.Cities);
            InsertWeatherData.Insert(datas.WeatherDatas);

            Console.WriteLine("Dodano!");
            Console.ReadKey();
        }
    }
}
