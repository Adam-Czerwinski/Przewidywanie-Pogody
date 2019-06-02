using System;

namespace BusinessObject
{
    public class WeatherData
    {
        public int IdWeatherData { get; set; }
        public int CityId { get; set; }
        public DateTime Date { get; set; }
        public int Hour { get; set; }
        public double Temperature { get; }
        public int Humidity { get; }
        public WindDirections WindDirection { get; }
        public int WindSpeed { get; }
        public int Cloudy { get; }
        public int Visibility { get; }
        public DataTypes DataType { get; set; }

        public WeatherData(int id, int cityId, DateTime date, int hour,
            double temperature, int humidity, WindDirections windDirection, int windSpeed, int cloudy, int visibility, DataTypes dataType)
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
        public WeatherData(double temperature, int humidity, WindDirections windDirection,
            int windSpeed, int cloudy, int visibility)
        {
            WindDirection = windDirection;
            Temperature = temperature;
            Humidity = humidity;
            WindSpeed = windSpeed;
            Cloudy = cloudy;
            Visibility = visibility;
        }

        public override string ToString()
        {
            return $"Date: {Date}\n"
                + $"Hour: {Hour}\n"
                + $"Temperature: {Temperature}\n"
                + $"Humidity: {Humidity}\n"
                + $"WindDirection: {WindDirection}\n"
                + $"WindSpeed: {WindSpeed}\n"
                + $"Cloudy: {Cloudy}\n"
                + $"Visibility: {Visibility}\n"
                + $"Type: {DataType}";
                
        }
    }
}
