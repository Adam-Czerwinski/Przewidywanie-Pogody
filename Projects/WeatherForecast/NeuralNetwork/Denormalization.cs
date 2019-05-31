using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Denormalization
    {
        public Denormalization() { }

        public List<WeatherData> Denormalize(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, IReadOnlyList<City> Cities, IReadOnlyList<WeatherData> weathersIn, double a = 0.01)
        {
            #region dane pomocnicze

            List<WeatherData> weatherDatas = new List<WeatherData>();
            int[] min = new int[] { -30, 0, 0, 0, 0 };
            int[] max = new int[] { 40, 100, 25, 8, 10 };
            int[] regionN = new int[] { 0, 0, 0, 0, 1 };
            int[] regionE = new int[] { 0, 0, 0, 1, 0 };
            int[] regionC = new int[] { 0, 0, 1, 0, 0 };
            int[] regionW = new int[] { 0, 1, 0, 0, 0 };
            int[] regionS = new int[] { 1, 0, 0, 0, 0 };
            double[] wdNNE = new double[] { 0, 0, 0.25, 0.75 };
            double[] wdN = new double[] { 0, 0, 0, 1 };
            double[] wdNE = new double[] { 0, 0, 0.5, 0.5 };
            double[] wdENE = new double[] { 0, 0, 0.75, 0.25 };
            double[] wdE = new double[] { 0, 0, 1, 0 };
            double[] wdESE = new double[] { 0, 0.25, 0.75, 0 };
            double[] wdSE = new double[] { 0, 0.5, 0.5, 0 };
            double[] wdSSE = new double[] { 0, 0.75, 0.25, 0 };
            double[] wdS = new double[] { 0, 1, 0, 0 };
            double[] wdSSW = new double[] { 0.25, 0.75, 0, 0 };
            double[] wdSW = new double[] { 0.5, 0.5, 0, 0 };
            double[] wdWSW = new double[] { 0.75, 0.25, 0, 0 };
            double[] wdW = new double[] { 1, 0, 0, 0 };
            double[] wdWNW = new double[] { 0.75, 0, 0, 0.25 };
            double[] wdNW = new double[] { 0.5, 0, 0, 0.5 };
            double[] wdNNW = new double[] { 0.25, 0, 0, 0.75 };
            double[] wdC = new double[] { 0, 0, 0, 0 };

            //Regions region;
            double temperature;
            WindDirections windDirection = WindDirections.C;
            //musiałem zainicjować windDirection i cityId z powodu błędów przy wywołaniu 
            //konstruktora
            DateTime date;
            DataTypes dataTypes;

            int id, cityId = 0, hour, /*month, year,*/ humidity, windSpeed, cloudy, visibility;

            //int day;
            int[] yearDays = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] LeapyearDays = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            #endregion

            for (int i = 0; i < weatherDataNormalizeds.Count; i++)
            {
                id = weatherDataNormalizeds[i].IdWeatherDataNormalized;
                //region = DenormalizeRegionAndCity(weatherDataNormalizeds, Cities, regionN, regionE, regionC, regionW, regionS, ref cityId, i);
                cityId = weathersIn[i].CityId;
                //year = weatherDataNormalizeds[i].Year;
                //double tempMonth = weatherDataNormalizeds[i].Month;
                //month = Convert.ToInt32(weatherDataNormalizeds[i].Month);

                //if (month == 0)
                //    month = 12;

                //double temp = 0;
                //if (year % 4 == 0)
                //    DenormalizeDayLeapYear(weatherDataNormalizeds, a, month, i, tempMonth, out day, LeapyearDays, out temp);
                //else
                //    DenormalizeDay(weatherDataNormalizeds, a, month, i, tempMonth, out day, yearDays, out temp);


                //date = new DateTime(year, month, day);
                date = weathersIn[i].Date;
                hour = DenormalizeHour(weatherDataNormalizeds, i);
                temperature = DenormalizeTemperature(weatherDataNormalizeds, min, max, i);
                humidity = DenormalizeHumidity(weatherDataNormalizeds, min, max, i);
                windDirection = DenormalizeWindDirection(weatherDataNormalizeds, wdNNE, wdN, wdNE, wdENE, wdE, wdESE, wdSE, wdSSE, wdS, wdSSW, wdSW, wdWSW, wdW, wdWNW, wdNW, wdNNW, wdC, windDirection, i);
                windSpeed = DenormalizeWindSpeed(weatherDataNormalizeds, min, max, i);
                cloudy = DenormalizeCloudy(weatherDataNormalizeds, min, max, i);
                visibility = DenormalizeVisibility(weatherDataNormalizeds, min, max, i);
                dataTypes = weatherDataNormalizeds[i].DataType;

                weatherDatas.Add(new WeatherData(id, cityId, date, hour, temperature,
                    humidity, windDirection, windSpeed, cloudy, visibility, dataTypes));
            }

            return weatherDatas;
        }

        private void DenormalizeDay(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, double a, int month, int i, double tempMonth, out int day, int[] yearDays, out double temp)
        {
            temp = (((weatherDataNormalizeds[i].Date - a) * (387 - 22)) + 22) / yearDays[month - 1];
            day = Convert.ToInt32((temp - tempMonth) * yearDays[month - 1]);
            if (day == 0)
                day = yearDays[month - 1];
        }

        private void DenormalizeDayLeapYear(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, double a, int month, int i, double tempMonth, out int day, int[] LeapyearDays, out double temp)
        {
            temp = (((weatherDataNormalizeds[i].Date - a) * (388 - 22)) + 22) / LeapyearDays[month - 1];
            day = Convert.ToInt32((temp - tempMonth) * LeapyearDays[month - 1]);
            if (day == 0)
                day = LeapyearDays[month - 1];
        }

        private int DenormalizeHour(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, int i)
        {
            return Convert.ToInt32(weatherDataNormalizeds[i].Hour * 24);
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

        private WindDirections DenormalizeWindDirection(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, double[] wdNNE, double[] wdN, double[] wdNE, double[] wdENE, double[] wdE, double[] wdESE, double[] wdSE, double[] wdSSE, double[] wdS, double[] wdSSW, double[] wdSW, double[] wdWSW, double[] wdW, double[] wdWNW, double[] wdNW, double[] wdNNW, double[] wdC, WindDirections windDirection, int i)
        {
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdN))
                windDirection = WindDirections.N;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdNNE))
                windDirection = WindDirections.NNE;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdNE))
                windDirection = WindDirections.NE;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdENE))
                windDirection = WindDirections.ENE;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdE))
                windDirection = WindDirections.E;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdESE))
                windDirection = WindDirections.ESE;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdSE))
                windDirection = WindDirections.SE;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdSSE))
                windDirection = WindDirections.SSE;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdS))
                windDirection = WindDirections.S;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdSSW))
                windDirection = WindDirections.SSW;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdSW))
                windDirection = WindDirections.SW;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdWSW))
                windDirection = WindDirections.WSW;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdW))
                windDirection = WindDirections.W;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdWNW))
                windDirection = WindDirections.WNW;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdNW))
                windDirection = WindDirections.NW;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdNNW))
                windDirection = WindDirections.NNW;
            if (weatherDataNormalizeds[i].WindDirection.SequenceEqual(wdC))
                windDirection = WindDirections.C;
            return windDirection;
        }

        //private Regions DenormalizeRegionAndCity(IReadOnlyList<WeatherDataNormalized> weatherDataNormalizeds, IReadOnlyList<City> Cities, int[] regionN, int[] regionE, int[] regionC, int[] regionW, int[] regionS, ref int cityId, int i)
        //{
        //    //inicjalizacja, obojetnie co zeby nie wywalalo errora
        //    Regions region = Regions.C;
        //    if (weatherDataNormalizeds[i].Region.SequenceEqual(regionN))
        //    {
        //        region = Regions.N;
        //        cityId = CheckCityId(region, Cities);
        //    }

        //    if (weatherDataNormalizeds[i].Region.SequenceEqual(regionE))
        //    {
        //        region = Regions.E;
        //        cityId = CheckCityId(region, Cities);
        //    }
        //    if (weatherDataNormalizeds[i].Region.SequenceEqual(regionC))
        //    {
        //        region = Regions.C;
        //        cityId = CheckCityId(region, Cities);
        //    }
        //    if (weatherDataNormalizeds[i].Region.SequenceEqual(regionW))
        //    {
        //        region = Regions.W;
        //        cityId = CheckCityId(region, Cities);
        //    }
        //    if (weatherDataNormalizeds[i].Region.SequenceEqual(regionS))
        //    {
        //        region = Regions.S;
        //        cityId = CheckCityId(region, Cities);
        //    }

        //    return region;
        //}

        //private int CheckCityId(Regions regions, IReadOnlyList<City> Cities)
        //{
        //    int id = 0;
        //    for (int j = 0; j < Cities.Count; j++)
        //    {

        //        if (regions == Cities[j].Region)
        //        {
        //            id = Cities[j].IdCity;
        //        }
        //    }
        //    return id;
        //}
    }
}
