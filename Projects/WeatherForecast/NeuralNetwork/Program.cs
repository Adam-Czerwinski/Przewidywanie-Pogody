
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
        //ilość iteracji dla sieci
        private const int iterations = 10;

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
        /// <returns>Zwraca tablicę dwuwymiarową wszystko potasowane ale pogrupowane</returns>
        private static WeatherDataNormalized[][] Tasuj(WeatherDataNormalized[][,] wdn)
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
        private static void GrupujDane(WeatherDataNormalized[][] weatherDataBGN, WeatherDataNormalized[][,] weatherDataGN)
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
        private static void GrupujDane(WeatherData[][] weatherDataBG, WeatherData[][,] weatherDataG)
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
            Network network = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });


            #region Krok 1 - Uczenie sieci

            rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningData.txt",
                BusinessObject.DataTypes.Learning_data);

            WeatherData[][] weatherDataL = rd.WeatherLearningGroupedDatas;

            WeatherData[][,] weatherDataLG = new WeatherData[weatherDataL.GetLength(0)][,];
            GrupujDane(weatherDataL, weatherDataLG);

            WeatherDataNormalized[][] weatherDataLN = new WeatherDataNormalized[weatherDataL.Length][];
            for (int i = 0; i < weatherDataLN.Length; i++)
                weatherDataLN[i] = normalization.Normalize(weatherDataL[i], rd.Cities).ToArray();

            WeatherDataNormalized[][,] weatherDataLGN = new WeatherDataNormalized[weatherDataLN.GetLength(0)][,];
            GrupujDane(weatherDataLN, weatherDataLGN);
            WeatherDataNormalized[][] weatherDataLGNS = Tasuj(weatherDataLGN);

            UczSiec(weatherDataLGNS, network);

            #endregion


            #region Krok 2 - Testowanie sieci

            rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\TestingData.txt",
                BusinessObject.DataTypes.Testing_data);

            WeatherData[][] weatherDataT = rd.WeatherTestingGroupedDatas;
            WeatherData[][,] weatherDataTG = new WeatherData[weatherDataT.GetLength(0)][,];
            GrupujDane(weatherDataT, weatherDataTG);

            WeatherDataNormalized[][] weatherDataTN = new WeatherDataNormalized[weatherDataT.Length][];
            for (int i = 0; i < weatherDataTN.Length; i++)
                weatherDataTN[i] = normalization.Normalize(weatherDataT[i], rd.Cities).ToArray();

            WeatherDataNormalized[][,] weatherDataTGN = new WeatherDataNormalized[weatherDataTN.GetLength(0)][,];
            GrupujDane(weatherDataTN, weatherDataTGN);

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
                    Console.ReadKey();
                }
            }

            #endregion





            //Wybieranie najlepszych wag dla punktu startowego
            //const int ileSieci = 10;
            //Network[] networks = new Network[ileSieci];
            //float[] totalErrors = new float[ileSieci];
            //int indeksMin = ZnajdzIndeksSieciNajmniejszyTotalError(weatherDataLGN, networks, totalErrors);

            //float[][,] weights = networks[indeksMin].GetWeights();
            //string weightsAsString = PobierzWagi(weights);

            ////zapisuje wagi do pliku
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
                    network.BackProp(new float[neuronsOutput] {(float)weatherDataLearning[j][1].WindDirection[0], (float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[2], (float)weatherDataLearning[j][1].WindDirection[3],
                         (float)weatherDataLearning[j][1].Temperature,(float)weatherDataLearning[j][1].Humidity,(float)weatherDataLearning[j][1].WindSpeed,(float)weatherDataLearning[j][1].Cloudy,(float)weatherDataLearning[j][1].Visibility
                    });
                }


                if (((float)i / iterations) * 100 > percent)
                {
                    Console.Clear();
                    Console.WriteLine(percent + "%");
                    Console.WriteLine("Total Error: " + network.GetTotalError() + Environment.NewLine);
                    percent = (int)(((float)i / iterations) * 100);
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
 * S - dane potasowane
*/
