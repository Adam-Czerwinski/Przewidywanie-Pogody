
using BusinessObject;
using Database;
using DataInsert;
using NeuralNetwork.ActivationFunctions;
using System;
using System.IO;
using DataAccessLayer;

namespace NeuralNetwork
{
    public class Program
    {
        #region Ustawienia sieci do uczenia
        public const float LearningRate = 0.1f;
        //Funkcja aktywacji najlepiej tanh lub sigmoid
        public static ActivationFunctionClient ActivactionFunction = new ActivationFunctionClient(new TanHActivationFunction());
        private const int iterations = 40;

        //region*5, windDirection*4, date, hour, temp, humidity, windSpeed, cloudy, visibility
        private const int neuronsInput = 16;
        private const int neuronsHidden = 35;
        //windDirection*4, temp, humidity, windSpeed, cloudy, visibility
        private const int neuronsOutput = 9;
        #endregion

        /// <summary>
        /// Tasuje dane
        /// </summary>
        /// <param name="wdn">Pogrupowane dane znormalizowane</param>
        private static void Tasuj(WeatherDataNormalized[][] wdn)
        {
            Random random = new Random();
            int zakres = wdn.Length;                         //wielkość listy
            WeatherDataNormalized[] temp;                    //pomocnicza zmienna przy zamianie
            int rnd;                                         //który indeks zamieniamy

            for (int i = 0; i < zakres; i++)
            {
                rnd = random.Next(0, zakres);
                temp = wdn[i];
                wdn[i] = wdn[rnd];
                wdn[rnd] = temp;
            }

        }

        /// <summary>
        /// Grupuje dane, [i][0] to warstwa wejściowa, a [i][1] to wartwa wyjściowa
        /// </summary>
        /// <param name="weatherDataBGN">Które dane grupuje, Before Grouping Normalization</param>
        /// <param name="weatherDataGN">Do czego pogrupować, Grouped Normalization</param>
        private static void GrupujDane(WeatherDataNormalized[] weatherDataBGN, WeatherDataNormalized[][] weatherDataGN)
        {
            for (int i = 0; i < weatherDataGN.Length; i++)
            {
                weatherDataGN[i] = new WeatherDataNormalized[2];

                weatherDataGN[i][0] = weatherDataBGN[i];
                weatherDataGN[i][1] = weatherDataBGN[i + 1];
            }
        }

        /// <summary>
        /// Grupuje dane, [i][0] to warstwa wejściowa, a [i][1] to wartwa wyjściowa
        /// </summary>
        /// <param name="weatherDataBG">Które dane grupuje, Before Grouping</param>
        /// <param name="weatherDataG">Do czego pogrupować, Grouped</param>
        private static void GrupujDane(WeatherData[] weatherDataBG, WeatherData[][] weatherDataG)
        {
            for (int i = 0; i < weatherDataG.Length; i++)
            {
                weatherDataG[i] = new WeatherData[2];

                weatherDataG[i][0] = weatherDataBG[i];
                weatherDataG[i][1] = weatherDataBG[i + 1];
            }
        }

        static void Main(string[] args)
        {
            #region Testy wprowadzania danych dotyczących konfiguracji i peocesu uczenia sieci do bazy

            //// Test metody zwracającej ostatni index danej tabeli
            //Console.WriteLine(TableName.activation_functions.ToString() + " : " + Database.Database.GetLastIndex(TableName.activation_functions));
            //Console.WriteLine(TableName.cities.ToString() + " : " + Database.Database.GetLastIndex(TableName.cities));
            //Console.WriteLine(TableName.generations.ToString() + " : " + Database.Database.GetLastIndex(TableName.generations));
            //Console.WriteLine(TableName.learning_process.ToString() + " : " + Database.Database.GetLastIndex(TableName.learning_process));
            //Console.WriteLine(TableName.weather_data.ToString() + " : " + Database.Database.GetLastIndex(TableName.weather_data));
            //Console.WriteLine(TableName.weight.ToString() + " : " + Database.Database.GetLastIndex(TableName.weight));

            //// Test wprowadzania danych do tabeli generations
            //Generation.AddGeneration(16, 30, 6, 0.34, 2);
            //Generation.AddGeneration(12, 59, 6, 0.12, "reluactivationfunction");
            //Generation.AddGeneration(12, 59, 6, 0.12, "nowaNieznanafuncja");

            //// Test dodawania wag
            //Weight.AddWeight("0.2332 0.221 0.1232 0.123; 0.32 0.234 0.23432"); // tu najlepiej używać funkcji PobierzWagi 

            //// Test dodawanie learning procesu
            //LearningProcess.AddLearningProcess(0, 1, 0.23, false); // Rozważnie używać - myslec jakiej generacji i wag bd dotyczyc proces

            //Console.ReadKey();

            #endregion

            ReadDataFile rd = new ReadDataFile();
            Normalization normalization = new Normalization();
            //Denormalization denormalization = new Denormalization();
            Network network = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });

            #region Krok 1 - Uczenie sieci

            rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningData.txt",
                BusinessObject.DataTypes.Learning_data);

            WeatherData[] weatherDataL = rd.WeatherLearningDatas;
            WeatherData[][] weatherDataLG = new WeatherData[weatherDataL.Length - 1][];
            GrupujDane(weatherDataL, weatherDataLG);

            WeatherDataNormalized[] weatherDataLN = normalization.Normalize(weatherDataL, rd.Cities).ToArray();

            WeatherDataNormalized[][] weatherDataLGN = new WeatherDataNormalized[weatherDataLN.Length - 1][];
            GrupujDane(weatherDataLN, weatherDataLGN);
            Tasuj(weatherDataLGN);

            UczSiec(weatherDataLGN, network);

            #endregion



            //W.I.P. ----------------------------------

            //#region Krok 2 - Testowanie sieci

            //rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\TestingData.txt",
            //    BusinessObject.DataTypes.Testing_data);

            //WeatherData[] weatherDataT = rd.WeatherTestingDatas;
            //WeatherData[][] weatherDataTG = new WeatherData[weatherDataT.Length - 1][];
            //GrupujDane(weatherDataT, weatherDataTG);

            //WeatherDataNormalized[] weatherDataTN = normalization.Normalize(weatherDataT, rd.Cities).ToArray();

            //WeatherDataNormalized[][] weatherDataTGN = new WeatherDataNormalized[rd.WeatherTestingDatas.Length - 1][];
            //GrupujDane(weatherDataTN, weatherDataTGN);


            //for (int i = 0; i < weatherDataTGN.Length; i++)
            //{
            //    float[] PredictedNeuronsN = network.FeedForward(new float[neuronsInput] {weatherDataTGN[i][0].Region[0], weatherDataTGN[i][0].Region[1], weatherDataTGN[i][0].Region[2], weatherDataTGN[i][0].Region[3], weatherDataTGN[i][0].Region[4],
            //            (float)weatherDataTGN[i][0].WindDirection[0], (float)weatherDataTGN[i][0].WindDirection[1], (float)weatherDataTGN[i][0].WindDirection[2], (float)weatherDataTGN[i][0].WindDirection[3],
            //             (float)weatherDataTGN[i][0].Date,(float)weatherDataTGN[i][0].Hour,(float)weatherDataTGN[i][0].Temperature,(float)weatherDataTGN[i][0].Humidity,(float)weatherDataTGN[i][0].WindSpeed,(float)weatherDataTGN[i][0].Cloudy,(float)weatherDataTGN[i][0].Visibility});

            //    WeatherDataNormalized expectedWeatherDataN = weatherDataTGN[i][1];
            //    WeatherData expectedWeatherData = weatherDataTG[i][1];

            //}


            //#endregion




            //Wybieranie najlepszych wag dla punktu startowego
            const int ileSieci = 10;
            Network[] networks = new Network[ileSieci];
            float[] totalErrors = new float[ileSieci];
            int indeksMin = ZnajdzIndeksSieciNajmniejszyTotalError(weatherDataLGN, networks, totalErrors);

            float[][,] weights = networks[indeksMin].GetWeights();
            string weightsAsString = PobierzWagi(weights);

            //zapisuje wagi do pliku
            File.WriteAllText("..\\..\\..\\..\\..\\DatabaseSources\\Data\\WeightsTemp.txt", weightsAsString);





            Console.ReadKey();
        }

        /// <summary>
        /// Pobiera wagi sieci, która daje najmniejszy TotalError
        /// </summary>
        /// <param name="weights">Wagi danej sieci</param>
        /// <returns>Wagi jako string. Warstwa oddzielona jest srednikiem</returns>
        private static string PobierzWagi(float[][,] weights)
        {
            string weightsAsString = "";
            for (int i = 0; i < weights.GetLength(0); i++)
            {
                for (int j = 0; j < weights[i].GetLength(0); j++)
                    for (int k = 0; k < weights[i].GetLength(1); k++)
                        weightsAsString = weightsAsString + weights[i][j, k].ToString() + " ";

                //żeby na końcu nie dawało średnika
                if (i != weights.GetLength(0) - 1)
                    weightsAsString += ";";
            }

            return weightsAsString;
        }

        /// <summary>
        /// Znajduje najmniejszy totalError, po krótkiej chwili nauki n sieci.
        /// Pobiera wagi sieci, która ma najmniejszy błąd.
        /// </summary>
        /// <param name="weatherDataLearning">Pogrupowane oraz znormalizowane dane uczące</param>
        /// <param name="networks">sieci neuronowe</param>
        /// <param name="totalErrors">tablica TotalError'ów, tutaj będą zapisywane błędy</param>
        private static int ZnajdzIndeksSieciNajmniejszyTotalError(WeatherDataNormalized[][] weatherDataLearning, Network[] networks, float[] totalErrors)
        {

            int percent = 0;
            for (int i = 0; i < networks.Length; i++)
            {
                networks[i] = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });
                totalErrors[i] = 0.0f;

                for (int k = 0; k < iterations; k++)
                {

                    for (int j = 0; j < weatherDataLearning.Length; j++)
                    {
                        networks[i].FeedForward(new float[neuronsInput] {weatherDataLearning[j][0].Region[0], weatherDataLearning[j][0].Region[1], weatherDataLearning[j][0].Region[2], weatherDataLearning[j][0].Region[3], weatherDataLearning[j][0].Region[4],
                            (float)weatherDataLearning[j][0].WindDirection[0], (float)weatherDataLearning[j][0].WindDirection[1], (float)weatherDataLearning[j][0].WindDirection[2], (float)weatherDataLearning[j][0].WindDirection[3],
                             (float)weatherDataLearning[j][0].Date,(float)weatherDataLearning[j][0].Hour,(float)weatherDataLearning[j][0].Temperature,(float)weatherDataLearning[j][0].Humidity,(float)weatherDataLearning[j][0].WindSpeed,(float)weatherDataLearning[j][0].Cloudy,(float)weatherDataLearning[j][0].Visibility
                        });
                        networks[i].BackProp(new float[neuronsOutput] {(float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[2], (float)weatherDataLearning[j][1].WindDirection[3],
                             (float)weatherDataLearning[j][1].Temperature,(float)weatherDataLearning[j][1].Humidity,(float)weatherDataLearning[j][1].WindSpeed,(float)weatherDataLearning[j][1].Cloudy,(float)weatherDataLearning[j][1].Visibility
                        });
                    }

                    if (((float)k / iterations) * 100 > percent)
                        percent++;

                    //Po 10% pobiera TotalError i przechodzi do następnej sieci
                    if (percent == 10)
                    {
                        totalErrors[i] = networks[i].GetTotalError();
                        Console.WriteLine($"Sieć {i}: MSE: {totalErrors[i]}");
                        percent = 0;
                        break;
                    }
                }
            }

            //znalezienie która sieć ma najmniejszy błąd
            int indeksMin = 0;
            float totalMin = 10000.0f;
            for (int i = 0; i < totalErrors.Length; i++)
            {
                if (totalErrors[i] < totalMin)
                {
                    totalMin = totalErrors[i];
                    indeksMin = i;
                }
            }

            Console.WriteLine("Najmniejszy total Error: " + totalErrors[indeksMin] + " ma sieć nieuronowa o indeksie: " + indeksMin);

            return indeksMin;
        }

        /// <summary>
        /// Uczenie sieci
        /// </summary>
        /// <param name="weatherDataLearning">Dane uczące</param>
        /// <param name="network">Sieć która uczy</param>
        private static void UczSiec(WeatherDataNormalized[][] weatherDataLearning, Network network)
        {
            int percent = 0;
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < weatherDataLearning.Length; j++)
                {
                    network.FeedForward(new float[neuronsInput] {weatherDataLearning[j][0].Region[0], weatherDataLearning[j][0].Region[1], weatherDataLearning[j][0].Region[2], weatherDataLearning[j][0].Region[3], weatherDataLearning[j][0].Region[4],
                        (float)weatherDataLearning[j][0].WindDirection[0], (float)weatherDataLearning[j][0].WindDirection[1], (float)weatherDataLearning[j][0].WindDirection[2], (float)weatherDataLearning[j][0].WindDirection[3],
                         (float)weatherDataLearning[j][0].Date,(float)weatherDataLearning[j][0].Hour,(float)weatherDataLearning[j][0].Temperature,(float)weatherDataLearning[j][0].Humidity,(float)weatherDataLearning[j][0].WindSpeed,(float)weatherDataLearning[j][0].Cloudy,(float)weatherDataLearning[j][0].Visibility
                    });
                    network.BackProp(new float[neuronsOutput] {(float)weatherDataLearning[j][1].WindDirection[0], (float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[2], (float)weatherDataLearning[j][1].WindDirection[3],
                         (float)weatherDataLearning[j][1].Temperature,(float)weatherDataLearning[j][1].Humidity,(float)weatherDataLearning[j][1].WindSpeed,(float)weatherDataLearning[j][1].Cloudy,(float)weatherDataLearning[j][1].Visibility
                    });
                }


                if (((float)i / iterations) * 100 > percent)
                {

                    Console.Clear();
                    Console.WriteLine(percent + "%");
                    Console.WriteLine("Total Error: " + network.GetTotalError());
                    percent++;
                }

            }
        }

    }
}


/* 
 * L - dane uczące
 * T - dane testowe
 * G - dane pogrupowane
 * N - dane znormalizowane
*/