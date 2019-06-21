using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Denormalization
    {
        public Denormalization() { }

        public List<WeatherData> Denormalize(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds/*, IReadOnlyList<City> Cities, IReadOnlyList<WeatherData> weathersIn, double a = 0.01*/)
        {
            #region dane pomocnicze

            List<WeatherData> weatherDatas = new List<WeatherData>();
            int[] min = new int[] { -30, 0, 0, 0, 0 };
            int[] max = new int[] { 40, 100, 25, 8, 10 };


            List<double[]> wd = new List<double[]>();
            wd.Add(new double[] { 0, 0, 0, 0 }); //C
            wd.Add(new double[] { 0, 0, 0, 1 }); //N
            wd.Add(new double[] { 0, 0, 0.25, 0.75 }); //NNE
            wd.Add(new double[] { 0, 0, 0.5, 0.5 }); //NE
            wd.Add(new double[] { 0, 0, 0.75, 0.25 }); //ENE
            wd.Add(new double[] { 0, 0, 1, 0 }); // E
            wd.Add(new double[] { 0, 0.25, 0.75, 0 }); //ESE
            wd.Add(new double[] { 0, 0.5, 0.5, 0 }); //SE
            wd.Add(new double[] { 0, 0.75, 0.25, 0 }); //SSE
            wd.Add(new double[] { 0, 1, 0, 0 }); //S
            wd.Add(new double[] { 0.25, 0.75, 0, 0 }); //SSW
            wd.Add(new double[] { 0.5, 0.5, 0, 0 }); //SW
            wd.Add(new double[] { 0.75, 0.25, 0, 0 }); //WSW
            wd.Add(new double[] { 1, 0, 0, 0 }); //W
            wd.Add(new double[] { 0.75, 0, 0, 0.25 }); //WNW
            wd.Add(new double[] { 0.5, 0, 0, 0.5 }); //NW
            wd.Add(new double[] { 0.25, 0, 0, 0.75 }); //NNW



            double temperature;
            WindDirections windDirection = WindDirections.C;
            //musiałem zainicjować windDirection i cityId z powodu błędów przy wywołaniu 
            //konstruktora


            int humidity, windSpeed, cloudy, visibility;


            int[] yearDays = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] LeapyearDays = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            #endregion

            for (int i = 0; i < weatherDataNormalizeds.Count; i++)
            {

                temperature = DenormalizeTemperature(weatherDataNormalizeds, min, max, i);
                humidity = DenormalizeHumidity(weatherDataNormalizeds, min, max, i);
                windSpeed = DenormalizeWindSpeed(weatherDataNormalizeds, min, max, i);
                windDirection = DenormalizeWindDirection(weatherDataNormalizeds, wd, windSpeed, windDirection, i);
                cloudy = DenormalizeCloudy(weatherDataNormalizeds, min, max, i);
                visibility = DenormalizeVisibility(weatherDataNormalizeds, min, max, i);


                weatherDatas.Add(new WeatherData(temperature, humidity, windDirection, windSpeed, cloudy, visibility));
            }

            return weatherDatas;
        }


        private int DenormalizeVisibility(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, int[] min, int[] max, int i)
        {
            return Convert.ToInt32(weatherDataNormalizeds[i].Visibility * (max[4] - min[4]) + min[4]);
        }

        private int DenormalizeCloudy(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, int[] min, int[] max, int i)
        {
            return Convert.ToInt32(weatherDataNormalizeds[i].Cloudy * (max[3] - min[3]) + min[3]);
        }

        private int DenormalizeWindSpeed(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, int[] min, int[] max, int i)
        {
            return Convert.ToInt32(weatherDataNormalizeds[i].WindSpeed * (max[2] - min[2]) + min[2]);
        }

        private int DenormalizeHumidity(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, int[] min, int[] max, int i)
        {
            return Convert.ToInt32(weatherDataNormalizeds[i].Humidity * (max[1] - min[1]) + min[1]);
        }

        private double DenormalizeTemperature(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, int[] min, int[] max, int i)
        {
            return weatherDataNormalizeds[i].Temperature * (max[0] - min[0]) + min[0];
        }

        private WindDirections DenormalizeWindDirection(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, List<double[]> wd, int windSpeed,/*double[] wdNNE, double[] wdN, double[] wdNE, double[] wdENE, double[] wdE, double[] wdESE, double[] wdSE, double[] wdSSE, double[] wdS, double[] wdSSW, double[] wdSW, double[] wdWSW, double[] wdW, double[] wdWNW, double[] wdNW, double[] wdNNW, double[] wdC,*/ WindDirections windDirection, int i)
        {

            double error = 0;
            double minerror = 10.0d;
            int index = 1;

            for (int j = 0; j < wd.Count; j++)
            {

                for (int k = 0; k < 4; k++)
                {
                    error += Math.Pow((wd[j][k] - weatherDataNormalizeds[i].WindDirection[k]), 2);
                }
                error /= 4;

                if (error < minerror)
                {
                    minerror = error;
                    index = j;
                }

            }

            if (index == 1)
                windDirection = WindDirections.N;
            if (index == 2)
                windDirection = WindDirections.NNE;
            if (index == 3)
                windDirection = WindDirections.NE;
            if (index == 4)
                windDirection = WindDirections.ENE;
            if (index == 5)
                windDirection = WindDirections.E;
            if (index == 6)
                windDirection = WindDirections.ESE;
            if (index == 7)
                windDirection = WindDirections.SE;
            if (index == 8)
                windDirection = WindDirections.SSE;
            if (index == 9)
                windDirection = WindDirections.S;
            if (index == 10)
                windDirection = WindDirections.SSW;
            if (index == 11)
                windDirection = WindDirections.SW;
            if (index == 12)
                windDirection = WindDirections.WSW;
            if (index == 13)
                windDirection = WindDirections.W;
            if (index == 14)
                windDirection = WindDirections.WNW;
            if (index == 15)
                windDirection = WindDirections.NW;
            if (index == 16)
                windDirection = WindDirections.NNW;

            // C to wartość która powinna występować tylko w przypadku braku wiatru - w tym celu do parametrów funkcji DenormalizedDirections dodałem wind speed
            if (windSpeed == 0)
                windDirection = WindDirections.C;

            return windDirection;
        }


    }
}
