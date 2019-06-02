using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BusinessObject;

namespace DataInsert
{
    public class ReadDataFile
    {
        public WeatherData[] WeatherLearningDatas { get; private set; }
        public WeatherData[] WeatherTestingDatas { get; private set; }
        public WeatherData[][] WeatherLearningGroupedDatas { get; private set; }
        public WeatherData[][] WeatherTestingGroupedDatas { get; private set; }
        public City[] Cities { get; private set; }
        public string[] ActivationFunctions { get; private set; }

        public ReadDataFile() { }

        /// <summary>
        /// Pobiera listę funkcji aktywacyjnych i zapisuje do ActivationFunctions
        /// </summary>
        public void LoadActivationFunctions()
        {
            string[] listOfFunction = null;
            try
            {
                listOfFunction = Directory.GetFiles("..\\..\\..\\NeuralNetwork\\ActivationFunctions");
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError: " + e + '\n');
            }

            ActivationFunctions = new string[listOfFunction.Length - 1];

            // zapis nazw funkcji do ActivationFunctions
            int n = 0; // index tablicy 
            foreach(var s in listOfFunction)
            {
                listOfFunction[n] = s.Remove(s.Length - 3);
                listOfFunction[n] = listOfFunction[n].Remove(0, "..\\..\\..\\NeuralNetwork\\ActivationFunctions".Length + 1);
                if ( n > 0)
                {
                    ActivationFunctions[n - 1] = listOfFunction[n];
                }
                n++;
            }
        }

        /// <summary>
        /// Pobiera dane ze sciezki path i zapisuje je z okreslonym typem dataType do tablic obiktow WeatherData
        /// </summary>
        /// <param name="path"></param>
        /// <param name="weatherDatas"></param>
        /// <param name="dataType"></param>
        public void LoadData(string path, DataTypes dataType)
        {
            StreamReader file = null;
            try
            {
                file = new StreamReader(path, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError: " + e + '\n');
            }

            string data;
            string[] var;
            int idWeatherData = 1;
            int idCity;

            List<City> _cities = new List<City>();
            List<WeatherData> _weatherDatas = new List<WeatherData>();

            if (Cities == null)
            {
                idCity = 1;
            }
            else
            {
                idCity = Cities.Length - 1;
                _cities.AddRange(Cities);
            }

            if (file != null)
            {
                while ((data = file.ReadLine()) != null && data != "")
                {
                    try
                    {
                        var = data.Split('\t');

                        // Dodawanie miast do _cities
                        City city = _cities.Find(x => x.Name == var[0]);

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
                            _cities.Add(city);
                            idCity++;
                        }

                        // Dodawanie danych pogodowych do _weatherDatas

                        #region Exception braku pomiaru wiatru

                        if (var[7] == "")
                            var[7] = "C";

                        #endregion

                        _weatherDatas.Add(new WeatherData(
                            idWeatherData,
                            city.IdCity,
                            new DateTime(int.Parse(var[1]), int.Parse(var[2]), int.Parse(var[3])),
                            int.Parse(var[4]), // Godzina
                            double.Parse(var[5].Replace('.', ',')), // Temperatura
                            int.Parse(var[6]), // Wilgotoność
                            (WindDirections)Enum.Parse(typeof(WindDirections), var[7]),
                            int.Parse(var[8]), // Prędkość wiatru
                            int.Parse(var[9]), // Zachmurzenie 
                            int.Parse(var[10]), dataType)); // Widzialność

                        idWeatherData++;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message + "\n");
                        Console.ReadKey();
                    }
                }
            }

            Cities = _cities.ToArray();

            // Grupowanie danych 

            List<WeatherData> w1 = new List<WeatherData>();
            List<WeatherData> w2 = new List<WeatherData>();
            List<WeatherData> w3 = new List<WeatherData>();
            List<WeatherData> w4 = new List<WeatherData>();
            List<WeatherData> w5 = new List<WeatherData>();

            foreach (WeatherData w in _weatherDatas)
            {
                switch (w.CityId)
                {
                    case 1: w1.Add(w);
                        break;
                    case 2:
                        w2.Add(w);
                        break;
                    case 3:
                        w3.Add(w);
                        break;
                    case 4:
                        w4.Add(w);
                        break;
                    case 5:
                        w5.Add(w);
                        break;
                }
            }

            if (dataType == DataTypes.Learning_data)
            {
                WeatherLearningDatas = _weatherDatas.ToArray();
                WeatherLearningGroupedDatas = new WeatherData[5][];
                WeatherLearningGroupedDatas[0] = w1.ToArray();
                WeatherLearningGroupedDatas[1] = w2.ToArray();
                WeatherLearningGroupedDatas[2] = w3.ToArray();
                WeatherLearningGroupedDatas[3] = w4.ToArray();
                WeatherLearningGroupedDatas[4] = w5.ToArray();
            }
            if (dataType == DataTypes.Testing_data)
            {
                WeatherTestingDatas = _weatherDatas.ToArray();
                WeatherTestingGroupedDatas = new WeatherData[5][];
                WeatherTestingGroupedDatas[0] = w1.ToArray();
                WeatherTestingGroupedDatas[1] = w2.ToArray();
                WeatherTestingGroupedDatas[2] = w3.ToArray();
                WeatherTestingGroupedDatas[3] = w4.ToArray();
                WeatherTestingGroupedDatas[4] = w5.ToArray();
            }
        }
    }
}
