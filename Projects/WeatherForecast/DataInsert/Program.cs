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

            Console.WriteLine("Trwa dodawanie...");

            // ladowanie danych z plikow
            datas.LoadActivationFunctions();
            datas.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningData.txt", BusinessObject.DataTypes.Learning_data);
            datas.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\TestingData.txt", BusinessObject.DataTypes.Testing_data);

            //wrzucanie danych do bazy
            ActivationFunctionRepository.Add(datas.ActivationFunctions);
            CityRepository.Add(datas.Cities);
            WeatherDataRepository.Add(datas.WeatherLearningDatas);
            WeatherDataRepository.Add(datas.WeatherTestingDatas);

            Console.WriteLine("Dodano!");
            Console.ReadKey();
        }
    }
}
