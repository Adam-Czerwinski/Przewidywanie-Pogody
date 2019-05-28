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

            // ladowanie danych z plikow
            datas.LoadActivationFunctions();
            datas.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningData.txt", BusinessObject.DataTypes.Learning_data);
            datas.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\TestingData.txt", BusinessObject.DataTypes.Testing_data);

            //wrzucanie danych do bazy
            ActivationFunctionRepository.Save(datas.ActivationFunctions);
            CityRepository.Save(datas.Cities);
            WeatherDataRepository.Save(datas.WeatherLearningDatas);
            WeatherDataRepository.Save(datas.WeatherTestingDatas);

            Console.WriteLine("Dodano!");
            Console.ReadKey();
        }
    }
}
