using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class WeatherDataNormalized
    {
        public int[] Region { get; }
        public double Date { get; }
        public double Hour { get; }
        public double Temperature { get; }
        public double Humidity { get; }
        public double[] WindDirection { get; }
        public double WindSpeed { get; }
        public double Cloudy { get; }
        public double Visibility { get; }

        public WeatherDataNormalized(int[] region, double date, double hour, double temperature, double humidity, double[] windDirection, double windSpeed, 
            double cloudy, double visibility)
        {
            Region = new int[5];
            region.CopyTo(Region, 0);

            Date = date;
            Hour = hour;
            Temperature = temperature;
            Humidity = humidity;

            WindDirection = new double[4];
            windDirection.CopyTo(WindDirection, 0);

            WindSpeed = windSpeed;
            Cloudy = cloudy;
            Visibility = visibility;
        }
    }
}
