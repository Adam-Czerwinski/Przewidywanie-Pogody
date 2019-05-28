using DataInsert;
using NeuralNetwork.ActivationFunctions;
using System.Diagnostics;
using System;

namespace NeuralNetwork
{
    public class Program
    {
        public static float LearningRate = 0.01f;
        //Patrzcie jaki fajny strategy pattern
        public static ActivationFunctionClient ActivactionFunction = new ActivationFunctionClient(new TanHActivationFunction());

        static void Main(string[] args)
        {

            ReadDataFile rd = new ReadDataFile();
            
            WeatherData[][] weatherDataLearning = new WeatherData[rd.LoadData().][2] 












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
