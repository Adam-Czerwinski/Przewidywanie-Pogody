using DataAccessLayer;
using DataInsert;
using System;

namespace DataIsnert
{
    public class Program
    {
        static void Main(string[] args)
        {

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
            CityRepository.Save(datas.Cities);
            WeatherDataRepository.Save(datas.WeatherDatas);

            Console.WriteLine("Dodano!");
            Console.ReadKey();
        }
    }
}
