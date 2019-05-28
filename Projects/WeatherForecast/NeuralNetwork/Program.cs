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
        public static float LearningRate = 0.01f;
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
            WeatherDataNormalized[] weatherDataNormalized = normalization.Normalize(rd.WeatherLearningDatas,rd.Cities).ToArray();

            //jeżeli dziele przed 2 i wychodzi int to zaokrągla w dól.
            //zamysł jest taki, że [i][0] to wejście, a [i][1] to wyjście
            WeatherDataNormalized[][] weatherDataLearning = new WeatherDataNormalized[rd.WeatherLearningDatas.Length/2][];

            //zaokrąglanie w dól pozwala na brak żadnych if'ów, żeby sprawdzac czy obiekt wskazuje na null np. przy i+1
            for(int i=0;i< weatherDataLearning.Length;i++)
            {
                weatherDataLearning[i] = new WeatherDataNormalized[2];

                weatherDataLearning[i][0] = weatherDataNormalized[i];
                weatherDataLearning[i][1] = weatherDataNormalized[i+1];
            }

            //przetasuj tablice
            Tasuj(weatherDataLearning);



            //#region Przykładowe działanie sieci neuronowej

            //Network net = new Network(new int[] { 3, 10, 1 }); //inicjalizacja sieci

            ////BRAMKA XOR
            ////15 000 iteracji dla każdego możliwego wyniku
            ////15000*8 = 120000 Operacji naukowych
            //for (int i = 0; i < 10000; i++)
            //{
            //    net.FeedForward(new float[] { 0, 0, 0 });
            //    net.BackProp(new float[] { 0 });

            //    net.FeedForward(new float[] { 0, 0, 1 });
            //    net.BackProp(new float[] { 1 });

            //    net.FeedForward(new float[] { 0, 1, 0 });
            //    net.BackProp(new float[] { 1 });

            //    net.FeedForward(new float[] { 0, 1, 1 });
            //    net.BackProp(new float[] { 0 });

            //    net.FeedForward(new float[] { 1, 0, 0 });
            //    net.BackProp(new float[] { 1 });

            //    net.FeedForward(new float[] { 1, 0, 1 });
            //    net.BackProp(new float[] { 0 });

            //    net.FeedForward(new float[] { 1, 1, 0 });
            //    net.BackProp(new float[] { 0 });

            //    net.FeedForward(new float[] { 1, 1, 1 });
            //    net.BackProp(new float[] { 1 });
            //}

            ////Test
            //Debug.WriteLine(net.FeedForward(new float[] { 0, 0, 0 })[0]);
            //Debug.WriteLine(net.FeedForward(new float[] { 0, 0, 1 })[0]);
            //Debug.WriteLine(net.FeedForward(new float[] { 0, 1, 0 })[0]);
            //Debug.WriteLine(net.FeedForward(new float[] { 0, 1, 1 })[0]);
            //Debug.WriteLine(net.FeedForward(new float[] { 1, 0, 0 })[0]);
            //Debug.WriteLine(net.FeedForward(new float[] { 1, 0, 1 })[0]);
            //Debug.WriteLine(net.FeedForward(new float[] { 1, 1, 0 })[0]);
            //Debug.WriteLine(net.FeedForward(new float[] { 1, 1, 1 })[0]);
            //Debug.WriteLine("");
            //Debug.WriteLine(net.GetMaxTotalError());
            //Debug.WriteLine(net.GetMinTotalError());
            //Debug.WriteLine(net.GetTotalError());

            //#endregion

        }
    }
}
