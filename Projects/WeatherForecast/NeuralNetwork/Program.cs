
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
        public const float LearningRate = 0.05f;
        //Funkcja aktywacji najlepiej
        public static ActivationFunctionClient ActivactionFunction = new ActivationFunctionClient(new TanHActivationFunction());
        public static string ActivationFunctionName = "TanH";
        //ilość iteracji dla sieci
        private const int iterations = 150;
        private const int iterationsForStartupSettings = 30;
        //region*5, windDirection*4, date, hour, temp, humidity, windSpeed, cloudy, visibility
        private const int neuronsInput = 16;
        private const int neuronsHidden = 25;
        //windDirection*4, temp, humidity, windSpeed, cloudy, visibility
        private const int neuronsOutput = 9;
        #endregion

        /// <summary>
        /// Tasuje dane
        /// </summary>
        /// <param name="wdn">Pogrupowane dane znormalizowane</param>
        /// <returns>Zwraca tablicę dwuwymiarową wszystko potasowane ale pogrupowane</returns>
        private static WeatherDataNormalized[][] Shuffle(WeatherDataNormalized[][,] wdn)
        {
            int sumaDlugosci = 0;
            for (int i = 0; i < wdn.GetLength(0); i++)
                sumaDlugosci += wdn[i].GetLength(0);

            WeatherDataNormalized[][] potasowane = new WeatherDataNormalized[sumaDlugosci][];

            //wrzucenie wszystkiego do jednej tablicy
            int l = 0;
            int k = 0;
            for (int j = 0; j < sumaDlugosci; j++, k++)
            {
                if (wdn[l].GetLength(0) * (l + 1) == j)
                {
                    k = 0;
                    l++;
                }
                potasowane[j] = new WeatherDataNormalized[2];
                potasowane[j][0] = wdn[l][k, 0];
                potasowane[j][1] = wdn[l][k, 1];
            }

            Random random = new Random();
            int zakres = potasowane.Length;                         //wielkość listy
            WeatherDataNormalized[] temp;                    //pomocnicza zmienna przy zamianie
            int rnd;                                         //który indeks zamieniamy


            for (int i = 0; i < zakres; i++)
            {
                rnd = random.Next(0, zakres);
                temp = potasowane[i];
                potasowane[i] = potasowane[rnd];
                potasowane[rnd] = temp;
            }

            return potasowane;
        }

        /// <summary>
        /// Grupuje dane, [i][j,0] to warstwa wejściowa, a [i][j,1] to wartwa wyjściowa
        /// </summary>
        /// <param name="weatherDataBGN">Które dane grupuje, Before Grouping Normalization</param>
        /// <param name="weatherDataGN">Do czego pogrupować, Grouped Normalization</param>
        private static void GroupData(WeatherDataNormalized[][] weatherDataBGN, WeatherDataNormalized[][,] weatherDataGN)
        {
            for (int i = 0; i < weatherDataGN.GetLength(0); i++)
            {
                weatherDataGN[i] = new WeatherDataNormalized[weatherDataBGN[i].Length - 1, 2];
                for (int j = 0; j < weatherDataGN[i].GetLength(0); j++)
                {
                    weatherDataGN[i][j, 0] = weatherDataBGN[i][j];
                    weatherDataGN[i][j, 1] = weatherDataBGN[i][j + 1];
                }
            }
        }

        /// <summary>
        /// Grupuje dane, [i][j,0] to warstwa wejściowa, a [i][j,1] to wartwa wyjściowa
        /// </summary>
        /// <param name="weatherDataBG">Które dane grupuje, Before Grouping</param>
        /// <param name="weatherDataG">Do czego pogrupować, Grouped</param>
        private static void GroupData(WeatherData[][] weatherDataBG, WeatherData[][,] weatherDataG)
        {
            for (int i = 0; i < weatherDataG.GetLength(0); i++)
            {
                weatherDataG[i] = new WeatherData[weatherDataBG[i].Length - 1, 2];
                for (int j = 0; j < weatherDataG[i].GetLength(0); j++)
                {
                    weatherDataG[i][j, 0] = weatherDataBG[i][j];
                    weatherDataG[i][j, 1] = weatherDataBG[i][j + 1];
                }
            }
        }

        static void Main(string[] args)
        {
            ReadDataFile rd = new ReadDataFile();
            Normalization normalization = new Normalization();
            Denormalization denormalization = new Denormalization();

            //główna sieć do uczenia
            Network network = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });

            #region Krok 1 - Uczenie sieci
            string weightsAsString = "";
            rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningData.txt",
                BusinessObject.DataTypes.Learning_data);

            WeatherData[][] weatherDataL = rd.WeatherLearningGroupedDatas;
            WeatherData[][,] weatherDataLG = new WeatherData[weatherDataL.GetLength(0)][,];
            GroupData(weatherDataL, weatherDataLG);

            WeatherDataNormalized[][] weatherDataLN = new WeatherDataNormalized[weatherDataL.Length][];
            for (int i = 0; i < weatherDataLN.Length; i++)
                weatherDataLN[i] = normalization.Normalize(weatherDataL[i], rd.Cities).ToArray();

            WeatherDataNormalized[][,] weatherDataLGN = new WeatherDataNormalized[weatherDataLN.GetLength(0)][,];
            GroupData(weatherDataLN, weatherDataLGN);
            WeatherDataNormalized[][] weatherDataLGNS = Shuffle(weatherDataLGN);

            #region Krok 1.5 - wybór wag i danych uczących
            //Wybieranie najlepszych wag dla punktu startowego
            const int numberOfNetworksToTest = 30;
            Network bestNetwork = null;
            WeatherDataNormalized[][] bestDataset = null;

            Console.WriteLine($"Wybieranie najlepszych ustawień początkowych spośród {numberOfNetworksToTest} losowych inicjalizacji...");
            int bestIndex = FindBestStartupSettings(numberOfNetworksToTest, ref weatherDataLGN, ref bestNetwork, ref bestDataset);

            Console.WriteLine(Environment.NewLine + $"Sieć o indeksie {bestIndex} ma najlepsze ustawienia początkowe," +
                $" a jej błąd MSE wynosi {bestNetwork.GetTotalError()}" + Environment.NewLine);

            float[][,] bestWeights = bestNetwork.GetWeights();

            //zapisuje wagi do pliku
            weightsAsString = GetWeightsAsString(bestWeights);
            File.WriteAllText("..\\..\\..\\..\\..\\DatabaseSources\\Data\\bestWeights.txt", weightsAsString);

            network.SetWeights(bestWeights);
            weatherDataLGNS = bestDataset;
            #endregion

            Console.WriteLine("Rozpoczynam naukę głównej sieci...");
            LearnNetwork(weatherDataLGNS, network);
            //zapisuje wagi nauczone do pliku
            weightsAsString = GetWeightsAsString(network.GetWeights());
            File.WriteAllText("..\\..\\..\\..\\..\\DatabaseSources\\Data\\NeuralNetworkWeights.txt", weightsAsString);

            //zapisuje ustawienia sieci do pliku
            string networkSettings = neuronsInput.ToString() + " " + neuronsHidden.ToString() + " " + neuronsOutput.ToString();
            File.WriteAllText("..\\..\\..\\..\\..\\DatabaseSources\\Data\\NeuralNetworkSettings.txt", networkSettings);
            #endregion

            #region Krok 2 - Testowanie sieci

            rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\TestingData.txt",
                BusinessObject.DataTypes.Testing_data);

            WeatherData[][] weatherDataT = rd.WeatherTestingGroupedDatas;
            WeatherData[][,] weatherDataTG = new WeatherData[weatherDataT.GetLength(0)][,];
            GroupData(weatherDataT, weatherDataTG);
            WeatherDataNormalized[][] weatherDataTN = new WeatherDataNormalized[weatherDataT.Length][];
            for (int i = 0; i < weatherDataTN.Length; i++)
                weatherDataTN[i] = normalization.Normalize(weatherDataT[i], rd.Cities).ToArray();

            WeatherDataNormalized[][,] weatherDataTGN = new WeatherDataNormalized[weatherDataTN.GetLength(0)][,];
            GroupData(weatherDataTN, weatherDataTGN);
            TestNetwork(denormalization, network, weatherDataTG, weatherDataTGN);

            #endregion

            Console.WriteLine(Environment.NewLine + Environment.NewLine);
            Console.WriteLine("Koniec działania programu. Naciśnij dowolny przycisk, by zakończyć!");
            Console.ReadKey();
        }

        /// <summary>
        /// Testuje sieć
        /// </summary>
        /// <param name="denormalization"></param>
        /// <param name="network"></param>
        /// <param name="weatherDataTG"></param>
        /// <param name="weatherDataTGN"></param>
        private static void TestNetwork(Denormalization denormalization, Network network, WeatherData[][,] weatherDataTG, WeatherDataNormalized[][,] weatherDataTGN)
        {
            for (int i = 0; i < weatherDataTGN.GetLength(0); i++)
            {
                for (int j = 0; j < weatherDataTGN[i].GetLength(0); j++)
                {
                    //wartości neuronów
                    float[] PredictedNeuronsN = network.FeedForward(new float[neuronsInput] {weatherDataTGN[i][j,0].Region[0], weatherDataTGN[i][j,0].Region[1], weatherDataTGN[i][j,0].Region[2], weatherDataTGN[i][j,0].Region[3], weatherDataTGN[i][j,0].Region[4],
                        (float)weatherDataTGN[i][j,0].WindDirection[0], (float)weatherDataTGN[i][j,0].WindDirection[1], (float)weatherDataTGN[i][j,0].WindDirection[2], (float)weatherDataTGN[i][j,0].WindDirection[3],
                         (float)weatherDataTGN[i][j,0].Date,(float)weatherDataTGN[i][j,0].Hour,(float)weatherDataTGN[i][j,0].Temperature,(float)weatherDataTGN[i][j,0].Humidity,(float)weatherDataTGN[i][j,0].WindSpeed,(float)weatherDataTGN[i][j,0].Cloudy,(float)weatherDataTGN[i][j,0].Visibility});

                    //Oczekiwane dane pogodowe
                    WeatherData expectedWeatherData = weatherDataTG[i][j, 1];

                    //windDirection*4, temp, humidity, windSpeed, cloudy, visibility
                    WeatherDataNormalized predictedWeatherDataN = new WeatherDataNormalized(new double[] { PredictedNeuronsN[0], PredictedNeuronsN[1], PredictedNeuronsN[2], PredictedNeuronsN[3] },
                        PredictedNeuronsN[4], PredictedNeuronsN[5], PredictedNeuronsN[6], PredictedNeuronsN[7], PredictedNeuronsN[8]);
                    WeatherData predictedWeatherData = denormalization.Denormalize(new WeatherDataNormalized[] { predictedWeatherDataN })[0];

                    predictedWeatherData.DataType = DataTypes.Predicted_data;
                    predictedWeatherData.CityId = expectedWeatherData.CityId;

                    //przewiduje na kolejne 6 lub 12 godzin
                    if (weatherDataTG[i][j, 0].Hour == 6)
                        predictedWeatherData.Hour = 12;
                    else if (weatherDataTG[i][j, 0].Hour == 12)
                        predictedWeatherData.Hour = 18;
                    else
                        predictedWeatherData.Hour = 6;


                    int rok = weatherDataTG[i][j, 0].Date.Year;
                    int miesiac = weatherDataTG[i][j, 0].Date.Month;
                    int dzien = weatherDataTG[i][j, 0].Date.Day;

                    //jezeli przewidziało na kolejny dzień
                    if (predictedWeatherData.Hour == 6)
                    {
                        dzien++;

                        int[] Days = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                        int[] LeapYearDays = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };


                        //jezeli nieprzestępny
                        if (rok % 4 != 0)
                        {
                            //To znaczy ze przekroczyl zakres dni w danym miesiacy, czyli kolejny miesiac
                            if (dzien - Days[miesiac - 1] > 0)
                            {
                                miesiac++;
                                dzien = 1;
                            }
                        }
                        else
                        {
                            if (dzien - LeapYearDays[miesiac - 1] > 0)
                            {
                                dzien = 1;
                                miesiac++;
                            }
                        }


                    }

                    if (miesiac == 13)
                    {
                        miesiac = 1;
                        rok += 1;
                    }

                    predictedWeatherData.Date = new DateTime(rok, miesiac, dzien);

                    Console.WriteLine("Przewidziane: ");
                    Console.WriteLine(predictedWeatherData.ToString() + Environment.NewLine);
                    Console.WriteLine("Oczekiwane: ");
                    Console.WriteLine(expectedWeatherData.ToString() + Environment.NewLine);
                    Console.WriteLine("Wciśnij przycisk, by kontynować..." + Environment.NewLine);
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Pobiera wagi sieci, która daje najmniejszy TotalError
        /// </summary>
        /// <param name="weights">Wagi danej sieci</param>
        /// <returns>Wagi jako string. Warstwa oddzielona jest srednikiem</returns>
        private static string GetWeightsAsString(float[][,] weights)
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
        /// Znajduje najlepsze ustawienia sieci. 
        /// </summary>
        /// <param name="ileSieci">ilośc sieci do przeszukania</param>
        /// <param name="weatherDataLGN">Znormalizowane dane uczące, pogrupowane</param>
        /// <param name="bestNetwork">referencja do której zostanie przypisana najlepsza sieć</param>
        /// <param name="bestDataset">referencja do której zostanie przypisany najlepszy pogrupowany zbiór znormalizowanych danych uczących, potasowanych</param>
        /// <returns>Zwraca indeks najlepszej sieci</returns>
        private static int FindBestStartupSettings(int ileSieci, ref WeatherDataNormalized[][,] weatherDataLGN, ref Network bestNetwork, ref WeatherDataNormalized[][] bestDataset)
        {
            int percent = 0;
            Network[] networks = new Network[ileSieci];
            WeatherDataNormalized[][][] datasets = new WeatherDataNormalized[ileSieci][][];

            for (int i = 0; i < ileSieci; i++)
            {
                networks[i] = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });
                datasets[i] = Shuffle(weatherDataLGN);

                for (int k = 0; k < iterationsForStartupSettings; k++)
                {
                    for (int j = 0; j < datasets[i].GetLength(0); j++)
                    {
                        networks[i].FeedForward(new float[neuronsInput] {datasets[i][j][0].Region[0], datasets[i][j][0].Region[1], datasets[i][j][0].Region[2], datasets[i][j][0].Region[3], datasets[i][j][0].Region[4],
                            (float)datasets[i][j][0].WindDirection[0], (float)datasets[i][j][0].WindDirection[1], (float)datasets[i][j][0].WindDirection[2], (float)datasets[i][j][0].WindDirection[3],
                             (float)datasets[i][j][0].Date,(float)datasets[i][j][0].Hour,(float)datasets[i][j][0].Temperature,(float)datasets[i][j][0].Humidity,(float)datasets[i][j][0].WindSpeed,(float)datasets[i][j][0].Cloudy,(float)datasets[i][j][0].Visibility
                        });
                        networks[i].BackProp(new float[neuronsOutput] {(float)datasets[i][j][1].WindDirection[1], (float)datasets[i][j][1].WindDirection[1], (float)datasets[i][j][1].WindDirection[2], (float)datasets[i][j][1].WindDirection[3],
                             (float)datasets[i][j][1].Temperature,(float)datasets[i][j][1].Humidity,(float)datasets[i][j][1].WindSpeed,(float)datasets[i][j][1].Cloudy,(float)datasets[i][j][1].Visibility
                        });
                    }

                    if (((float)k / iterationsForStartupSettings) * 100 > percent)
                        percent++;

                    Console.Write($"\rSieć {i + 1}/{networks.Length}\tTotal Error: {networks[i].GetTotalError()}   ");

                    //Po 3% pobiera TotalError i przechodzi do następnej sieci
                    if (percent >= 3)
                    {
                        percent = 0;
                        break;
                    }

                }
            }

            //początkowe przypisanie
            bestNetwork = networks[0];
            bestDataset = datasets[0];
            int indexOfBestNetwork = 0;

            //znalezienie najlepszej sieci i najlepszego datasetu
            for (int i = 0; i < ileSieci; i++)
            {
                if (networks[i].GetTotalError() < bestNetwork.GetTotalError())
                {
                    bestNetwork = networks[i];
                    bestDataset = datasets[i];
                    indexOfBestNetwork = i;
                }
            }

            return indexOfBestNetwork;
        }

        /// <summary>
        /// Uczenie sieci
        /// </summary>
        /// <param name="weatherDataLearning">Dane uczące</param>
        /// <param name="network">Sieć która uczy</param>
        private static void LearnNetwork(WeatherDataNormalized[][] weatherDataLearning, Network network)
        {

            //Jednorazowy zapis do bazy danych dot. ustawień sieci neuronowych
            GenerationRepository.Add(neuronsInput, neuronsHidden, neuronsOutput, LearningRate, ActivationFunctionName);


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

                Console.Write($"\r{percent}%   ");
                Console.Write($"\r{percent}% \t Total Error: {network.GetTotalError()}   ");

                if (((float)i / iterations) * 100 > percent)
                {
                    percent++;

                    //Zapisywanie do bazy danych co 10%
                    if (percent%10==0 || percent == 1)
                    {
                        //zapisywanie wag do bazy
                        string weightsAsString = GetWeightsAsString(network.GetWeights());
                        WeightRepository.Add(weightsAsString);

                        //zapisywanie postępu nauki
                        LearningProcessRepository.Add(i, network.GetTotalError(), false);
                    }
                }
            }

            //ostatnie zapisanie do bazy zeby bylo ze nauczona (skonczona jej nauka)
            LearningProcessRepository.Add(iterations, network.GetTotalError(), true);
        }

    }
}


/* 
 * L - learning data
 * T - testing data
 * G - grouped data
 * N - normalised data
 * S - shuffled data
*/
