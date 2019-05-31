using DataInsert;
using NeuralNetwork.ActivationFunctions;
using System.Diagnostics;
using System;
using BusinessObject;
using System.IO;

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
        /// <param name="weatherDataLearningNormalized">Dane uczące, znormalizowane, przed grupowaniem</param>
        /// <param name="weatherDataLearning">Dane uczące, znormalizowane, w nich będą pogrupowane dane</param>
        private static void GrupujDane(WeatherDataNormalized[] weatherDataLearningNormalized, WeatherDataNormalized[][] weatherDataLearning)
        {
            for (int i = 0; i < weatherDataLearning.Length; i++)
            {
                weatherDataLearning[i] = new WeatherDataNormalized[2];

                weatherDataLearning[i][0] = weatherDataLearningNormalized[i];
                weatherDataLearning[i][1] = weatherDataLearningNormalized[i + 1];
            }
        }

        static void Main(string[] args)
        {

            ReadDataFile rd = new ReadDataFile();
            rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningData.txt",
                BusinessObject.DataTypes.Learning_data);

            Normalization normalization = new Normalization();
            WeatherDataNormalized[] weatherDataLearningNormalized = normalization.Normalize(rd.WeatherLearningDatas, rd.Cities).ToArray();

            WeatherDataNormalized[][] weatherDataLearning = new WeatherDataNormalized[rd.WeatherLearningDatas.Length - 1][];
            GrupujDane(weatherDataLearningNormalized, weatherDataLearning);

            Tasuj(weatherDataLearning);

            //Network network = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });
            //UczSiec(weatherDataLearning, network);

            //Wybieranie najlepszych wag dla punktu startowego
            const int ileSieci = 10;
            Network[] networks = new Network[ileSieci];
            float[] totalErrors = new float[ileSieci];
            int indeksMin = ZnajdzIndeksSieciNajmniejszyTotalError(weatherDataLearning, networks, totalErrors);

            float[][,] weights = networks[indeksMin].GetWeights();
            string weightsAsString = PobierzWagi(weights);

            //zapisuje wagi do pliku
            //File.WriteAllText("..\\..\\..\\..\\..\\DatabaseSources\\Data\\WeightsTemp.txt", weightsAsString);

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
                    network.BackProp(new float[neuronsOutput] {(float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[2], (float)weatherDataLearning[j][1].WindDirection[3],
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
