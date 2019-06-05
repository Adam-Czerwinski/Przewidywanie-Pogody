using BusinessObject;
using System;
using System.IO;

namespace NeuralNetwork
{
    class ForecastWeather
    {
        private Network network;

        public ForecastWeather() { }

        public void SetupNetwork(string sciezkaSieci = "..\\..\\..\\..\\..\\DatabaseSources\\Data\\NeuralNetworkSettings.txt", string sciezkaWag = "..\\..\\..\\..\\..\\DatabaseSources\\Data\\bestWeights.txt")
        {
            ConfigNetwork(sciezkaSieci, sciezkaWag);
        }

        private void ConfigNetwork(string sciezka, string sciezkaWag)
        {
            string[] layers = null;
            try
            {
                layers = File.ReadAllText(sciezka).Split(' ');
            }
            catch (FileNotFoundException er)
            {
                Console.WriteLine(er.Message);
            }

            int iloscWarstwUkrytych = layers.Length - 2;

            int neuronyWarstwaWejsciowa = 0;
            int[] neuronyWarstwaUkryta = new int[iloscWarstwUkrytych];
            int neuronyWarstwaWyjsciowa = 0;

            int j = 0;
            for (int i = 0; i < layers.Length; i++)
            {
                //warstwa wejściowa
                if (i == 0)
                {
                    int.TryParse(layers[0], out neuronyWarstwaWejsciowa);
                    continue;
                }
                //warstwa wyjściowa
                if (i == layers.Length - 1)
                {
                    int.TryParse(layers[layers.Length - 1], out neuronyWarstwaWyjsciowa);
                    continue;
                }

                int.TryParse(layers[i], out neuronyWarstwaUkryta[j++]);

            }


            int[] construction = new int[layers.Length];

            for (int i = 0; i < construction.Length; i++)
            {
                if (i == 0)
                    construction[0] = neuronyWarstwaWejsciowa;
                else if (i != construction.Length - 1)
                    for (j = 0; j < neuronyWarstwaUkryta.Length; j++, i++)
                        construction[i] = neuronyWarstwaUkryta[j];

                if (i == construction.Length - 1)
                    construction[i] = neuronyWarstwaWyjsciowa;
            }

            network = new Network(construction);





            string weights = File.ReadAllText(sciezkaWag);
            string[] weightsSplittedBySpace = weights.Split(' ');

            float[][,] weightsFinally = new float[layers.Length-1][,];
            j = 0;
            //inicjalizacja
            for(int i=0;i<weightsFinally.GetLength(0);i++)
            {
                if(i==0)
                {
                    weightsFinally[i] = new float[neuronyWarstwaUkryta[0], neuronyWarstwaWejsciowa];
                    j++;
                    continue;
                }
                if(i==weightsFinally.Length-1)
                {
                    weightsFinally[i] = new float[neuronyWarstwaWyjsciowa, neuronyWarstwaUkryta[neuronyWarstwaUkryta.Length - 1]];
                    continue;
                }

                for(j=0;j<neuronyWarstwaUkryta.Length;j++,i++)
                {
                    weightsFinally[i] = new float[neuronyWarstwaUkryta[j], neuronyWarstwaUkryta[j-1]];
                }

            }

            int overall = 0;
            for(int i=0;i<weightsFinally.GetLength(0);i++)
            {
                for(j=0;j<weightsFinally[i].GetLength(0);j++)
                {
                    for(int k=0;k<weightsFinally[i].GetLength(1);k++,overall++)
                    {
                        if (weightsSplittedBySpace[overall] == "")
                            break;
                        else if(weightsSplittedBySpace[overall].Contains(";"))
                        {
                            float.TryParse(weightsSplittedBySpace[overall].TrimStart(';'), out weightsFinally[i][j, k]);
                            continue;
                        }

                        float.TryParse(weightsSplittedBySpace[overall], out weightsFinally[i][j, k]);
                    }
                }
            }
        }

        public void ForecastNextThreeDays(WeatherData weatherData)
        {
            //
        }
    }



}
