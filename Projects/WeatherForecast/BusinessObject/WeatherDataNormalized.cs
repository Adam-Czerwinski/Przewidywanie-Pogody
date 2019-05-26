using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class WeatherDataNormalized
    {

        public int IdWeatherDataNormalized { get; }
        public int[] Region { get; }
        public double Date { get; }

        public int Month { get; }
        public int Year { get; }
        public double Hour { get; }
        public double Temperature { get; }
        public double Humidity { get; }
        public double[] WindDirection { get; }
        public double WindSpeed { get; }
        public double Cloudy { get; }
        public double Visibility { get; }
        public DataTypes DataType { get; }



        public WeatherDataNormalized(int id, int[] region, double date, int month, int year, double hour,
            double temperature, double humidity, double[] windDirection,
            double windSpeed, double cloudy, double visibility, DataTypes dataType)
        {
            IdWeatherDataNormalized = id;
            Region = region;
            Date = date;
            Month = month;
            Year = year;
            Hour = hour;
            Temperature = temperature;
            Humidity = humidity;
            WindDirection = windDirection;
            WindSpeed = windSpeed;
            Cloudy = cloudy;
            Visibility = visibility;
            DataType = dataType;

        }
    }
}
