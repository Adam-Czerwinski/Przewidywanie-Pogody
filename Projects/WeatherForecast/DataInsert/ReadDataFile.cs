using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DataInsert
{
    class ReadDataFile
    {

        public List<WeatherData> WeatherDatas { get; }
        public List<City> Cities { get; }

        public ReadDataFile()
        {
            WeatherDatas = new List<WeatherData>();
            Cities = new List<City>();
            StreamReader file = null;

            try
            {
                file = new StreamReader("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningAndTestingData.txt", Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError: " + e + '\n');
            }

            string data;
            string[] var;
            int idWeatherData = 1;
            int idCity = 1;

            if (file != null)
            {
                while ((data = file.ReadLine()) != null && data != "")
                {
                    try
                    {
                        var = data.Split('\t');

                        // Dodawanie miast do Cities

                        City city = Cities.Find(x => x.Name == var[0]);

                        if (city == null)
                        {
                            Regions region;

                            switch (var[0])
                            {
                                case "BORUCINO":
                                    region = Regions.N;
                                    break;
                                case "BIAŁOWIEŻA":
                                    region = Regions.E;
                                    break;
                                case "PORONIN":
                                    region = Regions.S;
                                    break;
                                case "RADZIECHOWY":
                                    region = Regions.W;
                                    break;
                                default:
                                    region = Regions.C;
                                    break;
                            }

                            city = new City(idCity, var[0], region, true);
                            Cities.Add(city);
                            idCity++;
                        }

                        // Dodawanie danych pogodowych do WeatherData

                        DataTypes dataType;

                        if (var[1] == "2018" || var[1] == "2019")
                            dataType = DataTypes.Testing_data;
                        else
                            dataType = DataTypes.Learning_data;

                        #region Exception braku pomiaru wiatru

                        if (var[7] == "")
                            var[7] = "C";

                        #endregion

                        WeatherDatas.Add(new WeatherData(
                            idWeatherData,
                            city.IdCity,
                            new DateTime(int.Parse(var[1]), int.Parse(var[2]), int.Parse(var[3])),
                            int.Parse(var[4]), // Godzina
                            double.Parse(var[5].Replace('.', ',')), // Temperatura
                            int.Parse(var[6]), // Wilgotoność
                            (WindDirections)Enum.Parse(typeof(WindDirections), var[7]),
                            int.Parse(var[8]), // Prędkość wiatru
                            int.Parse(var[9]), // Zachmurzenie 
                            int.Parse(var[10]), // Widzialność
                            dataType));

                        idWeatherData++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message + "\n");
                        Console.ReadKey();
                    }
                }
            }
        }

    }
}
