using DataInsert;
using NeuralNetwork.ActivationFunctions;
using System.Diagnostics;
using System;
using BusinessObject;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public class Program
    {
        public static float LearningRate = 0.035f;
        //Patrzcie jaki fajny strategy pattern
        public static ActivationFunctionClient ActivactionFunction = new ActivationFunctionClient(new TanHActivationFunction());

        //tasuje weatherDataNormalized
        public static void Tasuj(WeatherDataNormalized[][] wdn)
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
            //Normalizuj dane
            WeatherDataNormalized[] weatherDataNormalized = normalization.Normalize(rd.WeatherLearningDatas, rd.Cities).ToArray();

            //jeżeli dziele przed 2 i wychodzi int to zaokrągla w dól.
            //zamysł jest taki, że [i][0] to wejście, a [i][1] to wyjście
            WeatherDataNormalized[][] weatherDataLearning = new WeatherDataNormalized[rd.WeatherLearningDatas.Length / 2][];

            //zaokrąglanie w dól pozwala na brak żadnych if'ów, żeby sprawdzac czy obiekt wskazuje na null np. przy i+1
            for (int i = 0; i < weatherDataLearning.Length; i++)
            {
                weatherDataLearning[i] = new WeatherDataNormalized[2];

                weatherDataLearning[i][0] = weatherDataNormalized[i];
                weatherDataLearning[i][1] = weatherDataNormalized[i + 1];
            }
            //przetasuj tablice
            Tasuj(weatherDataLearning);

            //region*5, windDirection*4, date, hour, temp, humidity, windDirection, windSpeed, cloudy, visibility
            const int neuronsInput = 16;
            const int neuronsHidden = 25;
            //windDirection*4, date, hour, temp, humidity, windDirection, windSpeed, cloudy, visibility
            const int neuronsOutput = 11;
            Network network = new Network(new int[] { neuronsInput, neuronsHidden, neuronsOutput });

            const int iterations = 1000;
            int procent = 0;
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < weatherDataLearning.Length; j++)
                {
                    network.FeedForward(new float[] {weatherDataLearning[j][0].Region[0], weatherDataLearning[j][0].Region[1], weatherDataLearning[j][0].Region[2], weatherDataLearning[j][0].Region[3], weatherDataLearning[j][0].Region[4],
                        (float)weatherDataLearning[j][0].WindDirection[0], (float)weatherDataLearning[j][0].WindDirection[1], (float)weatherDataLearning[j][0].WindDirection[2], (float)weatherDataLearning[j][0].WindDirection[3],
                         (float)weatherDataLearning[j][0].Date,(float)weatherDataLearning[j][0].Hour,(float)weatherDataLearning[j][0].Temperature,(float)weatherDataLearning[j][0].Humidity,(float)weatherDataLearning[j][0].WindSpeed,(float)weatherDataLearning[j][0].Cloudy,(float)weatherDataLearning[j][0].Visibility
                    });
                    network.BackProp(new float[] {(float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[1], (float)weatherDataLearning[j][1].WindDirection[2], (float)weatherDataLearning[j][1].WindDirection[3],
                         (float)weatherDataLearning[j][1].Date,(float)weatherDataLearning[j][1].Hour,(float)weatherDataLearning[j][1].Temperature,(float)weatherDataLearning[j][1].Humidity,(float)weatherDataLearning[j][1].WindSpeed,(float)weatherDataLearning[j][1].Cloudy,(float)weatherDataLearning[j][1].Visibility
                    });
                }

                //wyświetlanie co jeden procent
                if (((float)i / iterations) * 100 > procent)
                {
                    Console.Clear();
                    Console.WriteLine($"{procent.ToString()}%\nTotal Error: {network.GetTotalError()}\n Max Error: {network.GetMaxTotalError()}\tMin Error: {network.GetMinTotalError()}");
                    procent++;
                }

            }


        }
    }
}
