using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class Data : IData
    {
        public int Id { get; }
        public string Code { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public int Hour { get; }
        public double Temperature { get; }
        public int Humidity { get; }
        public string Direction { get; }
        public int Speed { get; }
        public int Cloudy { get; }
        public int Visibility { get; }

        public Data(int id, string code, string name, DateTime date, int hour, double temperature, int humidity, string direction, int speed, int cloudy, int visibility)
        {
            Id = id;
            Code = code;
            Name = name;
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

            Direction = direction;

            // Speed
            if (speed >= 0 && speed <= 25)
            {
                Speed = speed;
            }
            else
            {
                if (speed < 0)
                    Speed = 0;
                else
                    Speed = 25;
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
        }


        override
        public string ToString()
        {
            return
                "Id: " + Id
                + " Code: " + Code
                + " Name: " + Name
                + " Date: " + Date.ToShortDateString()
                + " Hour: " + Hour
                + '\n'
                + "Temperature: " + Temperature
                + " Humidity: " + Humidity
                + " Direction: " + Direction
                + " Speed: " + Speed
                + " Cloudy: " + Cloudy
                + " Visibility: " + Visibility 
                + "\n--------------------------------------------" 
                + "--------------------------------------------\n";
        }
    }
}
