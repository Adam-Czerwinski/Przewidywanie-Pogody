using DataInsert;
using NeuralNetwork.ActivationFunctions;
using System.Diagnostics;
using System;
using BusinessObject;

//do sprawdzenia funkcje: Swish, Relu

namespace NeuralNetwork
{
    public class Program
    {
        #region Ustawienia sieci
        public const float LearningRate = 0.1f;
        //tanh lub sigmoid
        public static ActivationFunctionClient ActivactionFunction = new ActivationFunctionClient(new TanHActivationFunction());
        private const int iterations = 300;

        //region*5, windDirection*4, date, hour, temp, humidity, windSpeed, cloudy, visibility
        private const int neuronsInput = 16;
        private const int neuronsHidden = 35;
        //windDirection*4, temp, humidity, windSpeed, cloudy, visibility
        private const int neuronsOutput = 9;
        #endregion

        //tasuje weatherDataLearningNormalized
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

        static void Main(string[] args)
        {

            ReadDataFile rd = new ReadDataFile();
            //wczytaj dane learning
            rd.LoadData("..\\..\\..\\..\\..\\DatabaseSources\\Data\\LearningData.txt",
                BusinessObject.DataTypes.Learning_data);

            Normalization normalization = new Normalization();
            //Normalizuj dane, czyli odzwierciedlaj wartości neuronów!!!!
            WeatherDataNormalized[] weatherDataLearningNormalized = normalization.Normalize(rd.WeatherLearningDatas, rd.Cities).ToArray();

            //zamysł jest taki, że [i][0] to warstwa wejściowa, a [i][1] to wartwa wyjściowa
            WeatherDataNormalized[][] weatherDataLearning = new WeatherDataNormalized[rd.WeatherLearningDatas.Length - 1][];
            for (int i = 0; i < weatherDataLearning.Length; i++)
            {
                weatherDataLearning[i] = new WeatherDataNormalized[2];

                weatherDataLearning[i][0] = weatherDataLearningNormalized[i];
                weatherDataLearning[i][1] = weatherDataLearningNormalized[i + 1];
            }
            //przetasuj tablice
            Tasuj(weatherDataLearning);

            Network network = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });
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



            //Wybieranie najlepszych wag dla punktu startowego
            //Network[] networks = new Network[100];
            //float[] totalErrors = new float[100];
            //int percent = 0;
            //for (int i = 0; i < 100; i++)
            //{
            //    networks[i] = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });
            //    totalErrors[i] = 0.0f;

            //    for (int k = 0; k < iterations; k++)
            //    {

            //        for (int j = 0; j < weatherDataLearning.Length; j++)
            //        {
            //            networks[i].FeedForward(new float[neuronsInput] {weatherDataLearning[j][0].Region[0], weatherDataLearning[j][0].Region[1], weatherDataLearning[j][0].Region[2], weatherDataLearning[j][0].Region[3], weatherDataLearning[j][0].Region[4],
            //                (float)weatherDataLearning[j][0].WindDirection[0], (float)weatherDataLearning[j][0].WindDirection[1], (float)weatherDataLearning[j][0].WindDirection[2], (float)weatherDataLearning[j][0].WindDirection[3],
            //                 (float)weatherDataLearning[j][0].Date,(float)weatherDataLearning[j][0].Hour,(float)weatherDataLearning[j][0].Temperature,(float)weatherDataLearning[j][0].Humidity,(float)weatherDataLearning[j][0].WindSpeed,(float)weatherDataLearning[j][0].Cloudy,(float)weatherDataLearning[j][0].Visibility
            //            });
            //            networks[i].BackProp(new float[neuronsOutput] {(float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[2], (float)weatherDataLearning[j][1].WindDirection[3],
            //                 (float)weatherDataLearning[j][1].Temperature,(float)weatherDataLearning[j][1].Humidity,(float)weatherDataLearning[j][1].WindSpeed,(float)weatherDataLearning[j][1].Cloudy,(float)weatherDataLearning[j][1].Visibility
            //            });
            //        }

            //        if (((float)k / iterations) * 100 > percent)
            //            percent++;


            //        if (percent == 10)
            //        {
            //            totalErrors[i] = networks[i].GetTotalError();
            //            Console.WriteLine(i + ": " + totalErrors[i]);
            //            percent = 0;
            //            break;
            //        }
            //    }
            //}

            ////znalezienie która sieć ma najmniejszy błąd
            //int indeksMin = 0;
            //float totalMin = 10000.0f;
            //for (int i = 0; i < totalErrors.Length; i++)
            //{
            //    if (totalErrors[i] < totalMin)
            //    {
            //        totalMin = totalErrors[i];
            //        indeksMin = i;
            //    }
            //}

            //Console.WriteLine("Najmniejszy total Error: " + totalErrors[indeksMin] + "ma sieć nieuronowa o indeksie: " + indeksMin);
            Console.ReadKey();
        }
    }
}
