using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInsert
{
    public enum WindDirections { C, E, N, S, W, NE, NW, SE, SW, ENE, ESE, NNE, NNW, SSE, SSW, WNW, WSW }

    public enum DataTypes { Learning_data, Testing_data, User_input_data, Recursive_data }

    public class WeatherData
    {

        public int IdWeatherData { get; }
        public int CityId { get; }
        public DateTime Date { get; }
        public int Hour { get; }
        public double Temperature { get; }
        public int Humidity { get; }
        public WindDirections WindDirection { get; }
        public int WindSpeed { get; }
        public int Cloudy { get; }
        public int Visibility { get; }
        public DataTypes DataType { get; }

        public WeatherData
            (
            int id, int cityId, DateTime date, int hour, 
            double temperature, int humidity, WindDirections windDirection, int windSpeed, int cloudy, int visibility, DataTypes dataType
            )
        {
            IdWeatherData = id;
            CityId = cityId;
            Date = date;
            Hour = hour;

            // Temperature
            if (temperature >= -30 && temperature <= 40)
            {
                Temperature = temperature;
            }
            else
            {
                if (temperature < -30)
                    Temperature = -30;
                else
                    Temperature = 40;
            }

            // Humidity
            if (humidity >= 0 && humidity <= 100)
            {
                Humidity = humidity;
            }
            else
            {
                if (humidity < 0)
                    Humidity = 0;
                else
                    Humidity = 100;
            }

            WindDirection = windDirection;

            // Wind speed
            if (windSpeed >= 0 && windSpeed <= 25)
            {
                WindSpeed = windSpeed;
            }
            else
            {
                if (windSpeed < 0)
                    WindSpeed = 0;
                else
                    WindSpeed = 25;
            }

            // Cloudy
            if (cloudy >= 0 && cloudy <= 8)
            {
                Cloudy = cloudy;
            }
            else
            {
                if (cloudy < 0)
                    Cloudy = 0;
                else
                    Cloudy = 8;
            }

            // Visibility
            if (visibility >= 0 && visibility <= 10)
            {
                Visibility = visibility;
            }
            else
            {
                if (visibility < 0)
                    Visibility = 0;
                else
                    Visibility = 10;
            }

            DataType = dataType;
        }


        public override string ToString()
        {
            return
                "Id: " + IdWeatherData
                + " City Id: " + CityId
                + " Date: " + Date.ToShortDateString()
                + " Hour: " + Hour
                + '\n'
                + "Temperature: " + Temperature
                + " Humidity: " + Humidity
                + " Wind direction: " + WindDirection
                + " Wind speed: " + WindSpeed
                + " Cloudy: " + Cloudy
                + " Visibility: " + Visibility
                + " Data type: " + DataType
                + "\n--------------------------------------------"
                + "--------------------------------------------\n";
        }

    }
}
