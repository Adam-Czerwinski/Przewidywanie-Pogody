using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Normalization
    {
        public Normalization() { }

        /// <summary>
        /// Normalizuje dane, czyli odzwierciedla wartości neuronów wejściowych/wyjściowych
        /// </summary>
        /// <param name="WeatherDatas">Dane pogodowe do znormalizowania</param>
        /// <param name="Cities">Miasta</param>
        /// <returns></returns>
        public List<WeatherDataNormalized> Normalize(IReadOnlyList<WeatherData> WeatherDatas, IReadOnlyList<City> Cities, double a = 0.01)
        {
            #region Dane pomocnicze
            List<WeatherDataNormalized> weatherDataNormalized = new List<WeatherDataNormalized>();

            int[] min = new int[] { -30, 0, 0, 0, 0 };
            int[] max = new int[] { 40, 100, 25, 8, 10 };
            int[] region = new int[5];
            int[] yearDays = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] LeapyearDays = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int id, year, month;

            double date, hour, temperature, humidity, cloudy, visibility, windSpeed;
            double[] windDirection = new double[4];
            DataTypes dataType;
            #endregion

            for (int i = 0; i < WeatherDatas.Count; i++)
            {
                for (int j = 0; j < Cities.Count; j++)
                {
                    region = NormalizeRegion(WeatherDatas, Cities, region, i, j);
                }

                year = WeatherDatas[i].Date.Year;
                month = WeatherDatas[i].Date.Month;
                double tempMonth = Convert.ToDouble(month);
                double tempDay = Convert.ToDouble(WeatherDatas[i].Date.Day);

                if (tempMonth == 12 && tempDay >= 22)
                    tempMonth = 0;

                date = NormalizeDate(a, yearDays, LeapyearDays, year, month, tempMonth, tempDay);
                // 0  będzie potrzebne do denormalizacji
                //month = Convert.ToInt32(tempMonth);

                //double tempDate = Convert.ToDouble(WeatherDatas[i].Date.DayOfYear)+22;
                //if (tempDate >= 376)
                //{
                //    tempDate -= 376;
                //}
                //date = ((tempDate - 1) / (366 - 1));

                hour = NormalizeHour(WeatherDatas, i);
                temperature = NormalizeTemperature(WeatherDatas, min, max, i);
                humidity = NormalizeHumidity(WeatherDatas, min, max, i);
                windDirection = NormalizeWindDirection(WeatherDatas, windDirection, i);
                windSpeed = NormalizeWindSpeed(WeatherDatas, min, max, i);
                cloudy = NormalizeCloudy(WeatherDatas, min, max, i);
                visibility = NormalizeVisibility(WeatherDatas, min, max, i);

                dataType = WeatherDatas[i].DataType;
                id = WeatherDatas[i].IdWeatherData;

                weatherDataNormalized.Add(new WeatherDataNormalized(id, region, date, /*month, year,*/ hour,
                    temperature, humidity, windDirection, windSpeed, cloudy, visibility, dataType));
            }

            return weatherDataNormalized;
        }

        #region prywatne metody 
        private double NormalizeDate(double a, int[] yearDays, int[] LeapyearDays, int year, int month, double tempMonth, double tempDay)
        {
            double date = 0;
            if (year % 4 == 0)
            {
                date = (tempMonth * LeapyearDays[month - 1] + tempDay - 22) / (388 - 22) + a;
            }
            else
                date = (tempMonth * yearDays[month - 1] + tempDay - 22) / (387 - 22) + a;
            return date;
        }

        private double NormalizeVisibility(IReadOnlyList<WeatherData> WeatherDatas, int[] min, int[] max, int i)
        {
            return (Convert.ToDouble(WeatherDatas[i].Visibility) - min[4]) / (max[4] - min[4]);
        }

        private double NormalizeCloudy(IReadOnlyList<WeatherData> WeatherDatas, int[] min, int[] max, int i)
        {
            return (Convert.ToDouble(WeatherDatas[i].Cloudy) - min[3]) / (max[3] - min[3]);
        }

        private double NormalizeWindSpeed(IReadOnlyList<WeatherData> WeatherDatas, int[] min, int[] max, int i)
        {
            return (Convert.ToDouble(WeatherDatas[i].WindSpeed) - min[2]) / (max[2] - min[2]);
        }

        private double NormalizeHumidity(IReadOnlyList<WeatherData> WeatherDatas, int[] min, int[] max, int i)
        {
            return (Convert.ToDouble(WeatherDatas[i].Humidity) - min[1]) / (max[1] - min[1]);
        }

        private double NormalizeTemperature(IReadOnlyList<WeatherData> WeatherDatas, int[] min, int[] max, int i)
        {
            return (Convert.ToDouble(WeatherDatas[i].Temperature) - min[0]) / (max[0] - min[0]);
        }

        private double NormalizeHour(IReadOnlyList<WeatherData> WeatherDatas, int i)
        {
            return Convert.ToDouble(WeatherDatas[i].Hour) / 24;
        }

        private double[] NormalizeWindDirection(IReadOnlyList<WeatherData> WeatherDatas, double[] windDirection, int i)
        {
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "N")
                windDirection = new double[] { 0, 0, 0, 1 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "NNE")
                windDirection = new double[] { 0, 0, 0.25, 0.75 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "NE")
                windDirection = new double[] { 0, 0, 0.5, 0.5 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "ENE")
                windDirection = new double[] { 0, 0, 0.75, 0.25 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "E")
                windDirection = new double[] { 0, 0, 1, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "ESE")
                windDirection = new double[] { 0, 0.25, 0.75, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "SE")
                windDirection = new double[] { 0, 0.5, 0.5, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "SSE")
                windDirection = new double[] { 0, 0.75, 0.25, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "S")
                windDirection = new double[] { 0, 1, 0, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "SSW")
                windDirection = new double[] { 0.25, 0.75, 0, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "SW")
                windDirection = new double[] { 0.5, 0.5, 0, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "WSW")
                windDirection = new double[] { 0.75, 0.25, 0, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "W")
                windDirection = new double[] { 1, 0, 0, 0 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "WNW")
                windDirection = new double[] { 0.75, 0, 0, 0.25 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "NW")
                windDirection = new double[] { 0.5, 0, 0, 0.5 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "NNW")
                windDirection = new double[] { 0.25, 0, 0, 0.75 };
            if (Convert.ToString(WeatherDatas[i].WindDirection) == "C")
                windDirection = new double[] { 0, 0, 0, 0 };
            return windDirection;
        }

        private int[] NormalizeRegion(IReadOnlyList<WeatherData> WeatherDatas, IReadOnlyList<City> Cities, int[] region, int i, int j)
        {
            if (WeatherDatas[i].CityId == Cities[j].IdCity)
            {
                if (Convert.ToString(Cities[j].Region) == "N")
                {
                    region = new int[] { 0, 0, 0, 0, 1 };
                }
                else if (Convert.ToString(Cities[j].Region) == "E")
                {
                    region = new int[] { 0, 0, 0, 1, 0 };
                }
                else if (Convert.ToString(Cities[j].Region) == "C")
                {
                    region = new int[] { 0, 0, 1, 0, 0 };
                }
                else if (Convert.ToString(Cities[j].Region) == "W")
                {
                    region = new int[] { 0, 1, 0, 0, 0 };
                }
                else if (Convert.ToString(Cities[j].Region) == "S")
                {
                    region = new int[] { 1, 0, 0, 0, 0 };
                }
            }

            return region;
        }
        #endregion
    }


}
