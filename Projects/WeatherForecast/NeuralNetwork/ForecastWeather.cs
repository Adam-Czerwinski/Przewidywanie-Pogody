using BusinessObject;
using DataAccessLayer;
using System;
using System.IO;

namespace NeuralNetwork
{
    class ForecastWeather
    {
        public Network network { get; set; }

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

            float[][,] weightsFinally = new float[layers.Length - 1][,];
            j = 0;
            //inicjalizacja
            for (int i = 0; i < weightsFinally.GetLength(0); i++)
            {
                if (i == 0)
                {
                    weightsFinally[i] = new float[neuronyWarstwaUkryta[0], neuronyWarstwaWejsciowa];
                    j++;
                    continue;
                }
                if (i == weightsFinally.Length - 1)
                {
                    weightsFinally[i] = new float[neuronyWarstwaWyjsciowa, neuronyWarstwaUkryta[neuronyWarstwaUkryta.Length - 1]];
                    continue;
                }

                for (j = 0; j < neuronyWarstwaUkryta.Length; j++, i++)
                {
                    weightsFinally[i] = new float[neuronyWarstwaUkryta[j], neuronyWarstwaUkryta[j - 1]];
                }

            }

            int overall = 0;
            for (int i = 0; i < weightsFinally.GetLength(0); i++)
            {
                for (j = 0; j < weightsFinally[i].GetLength(0); j++)
                {
                    for (int k = 0; k < weightsFinally[i].GetLength(1); k++, overall++)
                    {
                        if (weightsSplittedBySpace[overall] == "")
                            break;
                        else if (weightsSplittedBySpace[overall].Contains(";"))
                        {
                            float.TryParse(weightsSplittedBySpace[overall].TrimStart(';'), out weightsFinally[i][j, k]);
                            continue;
                        }

                        float.TryParse(weightsSplittedBySpace[overall], out weightsFinally[i][j, k]);
                    }
                }
            }

            network.SetWeights(weightsFinally);

        }

        public WeatherData[][] ForecastNextThreeDays(WeatherData weatherData)
        {
            //4 bo na 4 dni w tym dzisiejszy : )
            WeatherData[][] future = new WeatherData[4][];
            Normalization normalization = new Normalization();
            Denormalization denormalization = new Denormalization();
            City[] cities = CityRepository.getAll().ToArray();


            future[0] = new WeatherData[3];
            int ileRazyPrzewidywac = 0;
            int jakiDzienTablica = 0;
            int jakaGodzinaTablica = 0;

            if (weatherData.Hour == 6)
            {
                ileRazyPrzewidywac = 11;
                jakaGodzinaTablica = 1;
                future[0][0] = weatherData;
            }
            else if (weatherData.Hour == 12)
            {
                ileRazyPrzewidywac = 10;
                jakaGodzinaTablica = 2;
                future[0][1] = weatherData;
            }
            else
            {
                ileRazyPrzewidywac = 9;
                jakiDzienTablica = 1;
                future[0][2] = weatherData;
            }

            WeatherData input = weatherData;
            for (int i = 0; i < ileRazyPrzewidywac; i++, jakaGodzinaTablica++)
            {

                if (jakaGodzinaTablica == 3)
                {
                    jakaGodzinaTablica = 0;
                    jakiDzienTablica++;
                    future[jakiDzienTablica] = new WeatherData[3];
                }

                future[jakiDzienTablica][jakaGodzinaTablica] = ForecastNextWeather(input);
                input = future[jakiDzienTablica][jakaGodzinaTablica];
            }

            return future;
        }

        /// <summary>
        /// Przewiduje następną pogodę
        /// </summary>
        /// <param name="weatherData">na czym ma bazować</param>
        /// <returns>przewidziana pogoda</returns>
        public WeatherData ForecastNextWeather(WeatherData weatherData)
        {
            Normalization normalization = new Normalization();
            Denormalization denormalization = new Denormalization();
            City[] cities = CityRepository.getAll().ToArray();

            WeatherDataNormalized inputNormalized = normalization.Normalize(new WeatherData[] { weatherData }, cities)[0];

            //wartości neuronów
            float[] PredictedNeuronsN = network.FeedForward(new float[] {inputNormalized.Region[0], inputNormalized.Region[1], inputNormalized.Region[2], inputNormalized.Region[3], inputNormalized.Region[4],
                        (float)inputNormalized.WindDirection[0], (float)inputNormalized.WindDirection[1], (float)inputNormalized.WindDirection[2], (float)inputNormalized.WindDirection[3],
                         (float)inputNormalized.Date,(float)inputNormalized.Hour,(float)inputNormalized.Temperature,(float)inputNormalized.Humidity,(float)inputNormalized.WindSpeed,(float)inputNormalized.Cloudy,(float)inputNormalized.Visibility});


            WeatherDataNormalized outputNormalized = new WeatherDataNormalized(new double[] { PredictedNeuronsN[0], PredictedNeuronsN[1], PredictedNeuronsN[2], PredictedNeuronsN[3] },
                    PredictedNeuronsN[4], PredictedNeuronsN[5], PredictedNeuronsN[6], PredictedNeuronsN[7], PredictedNeuronsN[8]);

            WeatherData predictedWeatherData = denormalization.Denormalize(new WeatherDataNormalized[] { outputNormalized })[0];

            predictedWeatherData.DataType = DataTypes.Predicted_data;
            predictedWeatherData.CityId = weatherData.CityId;
            if (weatherData.Hour == 6)
                predictedWeatherData.Hour = 12;
            else if (weatherData.Hour == 12)
                predictedWeatherData.Hour = 18;
            else
                predictedWeatherData.Hour = 6;


            int rok = weatherData.Date.Year;
            int miesiac = weatherData.Date.Month;
            int dzien = weatherData.Date.Day;

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

            return predictedWeatherData;
        }
    }



}
