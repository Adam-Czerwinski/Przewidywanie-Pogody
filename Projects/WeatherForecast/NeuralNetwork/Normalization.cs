using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInsert;

namespace NeuralNetwork
{
    class Normalization
    {
        //dla niektórych pól są dwie listy na wejście i wyjście
        //z powodu różnych typów danych 
        private List<string> RegionIn;
        private List<int[]> RegionOut;
        private List<double> Month;
        private List<double> Day;
        private List<double> Date;
        private List<double> Hour;
        private List<double> Temperature;
        private List<double> Humidity;
        private List<string> WindDirectionIn;
        private List<double[]> WindDirectionOut;
        private List<double> WindSpeed;
        private List<double> Cloudy;
        private List<double> Visibility;


        
        public Normalization()
        {
            RegionIn = new List<string>();
            RegionOut = new List<int[]>();
            Month = new List<double>();
            Day = new List<double>();
            Date = new List<double>();
            Hour = new List<double>();
            Temperature = new List<double>();
            Humidity = new List<double>();
            WindDirectionIn = new List<string>();
            WindDirectionOut = new List<double[]>();
            WindSpeed = new List<double>();
            Cloudy = new List<double>();
            Visibility = new List<double>();
        }

        public void LoadRegions(List<City> dataList)
        {
            //ładowanie regionów z klasy city
            foreach (City city in dataList)
            {
                RegionIn.Add(Convert.ToString(city.Region));
            }
        }
        public void LoadWeatherData(List<WeatherData> dataList)
        {
            //ładowanie danych z pomiarów pogodowych
            foreach (WeatherData data in dataList)
            {
                           
                Day.Add((data.Date.Day));
                Month.Add((data.Date.Month));
                Hour.Add(Convert.ToDouble(data.Hour));
                Temperature.Add(Convert.ToDouble(data.Temperature));
                Humidity.Add(Convert.ToDouble(data.Humidity));
                WindDirectionIn.Add(Convert.ToString(data.WindDirection));
                WindSpeed.Add(Convert.ToDouble(data.WindSpeed));
                Cloudy.Add(Convert.ToDouble(data.Cloudy));
                Visibility.Add(Convert.ToDouble(data.Visibility));

            }

        }


        public void Norm(double a)
        {
            //minimalne i maksymalne wartości dla pól
            //Temperature, Humidity, WindSpeed, Cloudy i Visibility
            int[] min = new int[] { -30, 0, 0, 0, 0 };
            int[] max = new int[] { 40, 100, 25, 8, 10 };




            for (int i = 0; i < Temperature.Count(); i++)
            {
                //przydzielenie wartościom regionów odpowiednich tablic wag 
                if (RegionIn[i] == "N")
                    RegionOut.Add( new int[] { 0, 0, 0, 0, 1 });
                else if (RegionIn[i] == "E")
                    RegionOut.Add(new int[] { 0, 0, 0, 1, 0 });
                else if (RegionIn[i] == "C")
                    RegionOut.Add( new int[] { 0, 0, 1, 0, 0 });
                else if (RegionIn[i] == "W")
                    RegionOut.Add(new int[] { 0, 1, 0, 0, 0 });
                else if (RegionIn[i] == "S")
                    RegionOut.Add( new int[] { 1, 0, 0, 0, 0 });
                //normalizacja daty
                if (Month[i] == 12 && Day[i] >= 22)
                {
                    Month[i] = 0;
                }
                Date.Add(((Month[i] * 30 + Day[i] - 22) / (381 - 22)) + a);
                //normalizacja godziny, temperatury i wilgotności
                Hour[i] = Hour[i] / 24;
                Temperature[i] = (Temperature[i] - min[0]) / (max[0] - min[0]);
                Humidity[i] = (Humidity[i] - min[1]) / (max[1] - min[1]);
                //ustalenie odpowiednich tablic wag dla kierunku wiatru
                if (WindDirectionIn[i] == "N")
                    WindDirectionOut.Add(new double[] { 0, 0, 0, 1 });
                if (WindDirectionIn[i] == "NNE")
                    WindDirectionOut.Add(new double[] { 0, 0, 0.25, 0.75 });
                if (WindDirectionIn[i] == "NE")
                    WindDirectionOut.Add(new double[] { 0, 0, 0.5, 0.5 });
                if (WindDirectionIn[i] == "ENE")
                    WindDirectionOut.Add(new double[] { 0, 0, 0.75, 0.25 });
                if (WindDirectionIn[i] == "E")
                    WindDirectionOut.Add(new double[] { 0, 0, 1, 0 });
                if (WindDirectionIn[i] == "ESE")
                    WindDirectionOut.Add(new double[] { 0, 0.25, 0.75, 0 });
                if (WindDirectionIn[i] == "SE")
                    WindDirectionOut.Add(new double[] { 0, 0.5, 0.5, 0 });
                if (WindDirectionIn[i] == "SSE")
                    WindDirectionOut.Add(new double[] { 0, 0.75, 0.25, 0 });
                if (WindDirectionIn[i] == "S")
                    WindDirectionOut.Add(new double[] { 0, 1, 0, 0 });
                if (WindDirectionIn[i] == "SSW")
                    WindDirectionOut.Add(new double[] { 0.25, 0.75, 0, 0 });
                if (WindDirectionIn[i] == "SW")
                    WindDirectionOut.Add(new double[] { 0.5, 0.5, 0, 0 });
                if (WindDirectionIn[i] == "WSW")
                    WindDirectionOut.Add(new double[] { 0.75, 0.25, 0, 0 });
                if (WindDirectionIn[i] == "W")
                    WindDirectionOut.Add(new double[] { 1, 0, 0, 0 });
                if (WindDirectionIn[i] == "WNW")
                    WindDirectionOut.Add(new double[] { 0.75, 0, 0, 0.25 });
                if (WindDirectionIn[i] == "NW")
                    WindDirectionOut.Add(new double[] { 0.5, 0, 0, 0.5 });
                if (WindDirectionIn[i] == "NNW")
                    WindDirectionOut.Add(new double[] { 0.25, 0, 0, 0.75 });
                if (WindDirectionIn[i] == "C")
                    WindDirectionOut.Add(new double[] { 0, 0, 0, 0 });

                //normalizacja prędkości wiatru, zachmurzenia i widoczności
                WindSpeed[i] = (WindSpeed[i] - min[2]) / (max[2] - min[2]);
                Cloudy[i] = (Cloudy[i] - min[3]) / (max[3] - min[3]);
                Visibility[i] = (Visibility[i] - min[4]) / (max[4] - min[4]);
            }


        }

        public void DeNorm(double a)
        {

            //przy denormalizacji listy out są wejściem
            //są takie dwie i 3 in
            //reszta jest i wejściem i wyjście
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


            for (int i = 0; i < Temperature.Count(); i++)
            {
                if (RegionOut[i].SequenceEqual(regionN))
                    RegionIn.Add("N");
                if (RegionOut[i].SequenceEqual(regionE))
                    RegionIn.Add("E");
                if (RegionOut[i].SequenceEqual(regionC))
                    RegionIn.Add("C");
                if (RegionOut[i].SequenceEqual(regionW))
                    RegionIn.Add("W");
                if (RegionOut[i].SequenceEqual(regionS))
                    RegionIn.Add("S");
                double date = ((Date[i] - a) * (393 - 22)) / 31;
                double month = Math.Floor(date);
                if (month == 0)
                    Month.Add(12);
                else
                    Month.Add(Math.Floor(date));
                if ((date - month) * 31 == 0)
                    Day.Add(31);
                Day.Add(Convert.ToDouble(Convert.ToInt32((date - month) * 31)));
                               
                Hour[i] = Hour[i] * 24;
                Temperature[i] = Temperature[i] * (max[0] - min[0]) + min[0];
                Humidity[i] = (Humidity[i] - min[1]) / (max[1] - min[1]);
                if (WindDirectionOut[i].SequenceEqual(wdN))
                    WindDirectionIn.Add("N");
                if (WindDirectionOut[i].SequenceEqual(wdNNE))
                    WindDirectionIn.Add("NNE");
                if (WindDirectionOut[i].SequenceEqual(wdNE))
                    WindDirectionIn.Add("NE");
                if (WindDirectionOut[i].SequenceEqual(wdENE))
                    WindDirectionIn.Add("ENE");
                if (WindDirectionOut[i].SequenceEqual(wdE))
                    WindDirectionIn.Add("E");
                if (WindDirectionOut[i].SequenceEqual(wdESE))
                    WindDirectionIn.Add("ESE");
                if (WindDirectionOut[i].SequenceEqual(wdSE))
                    WindDirectionIn.Add("SE");
                if (WindDirectionOut[i].SequenceEqual(wdSSE))
                    WindDirectionIn.Add("SSE");
                if (WindDirectionOut[i].SequenceEqual(wdS))
                    WindDirectionIn.Add("S");
                if (WindDirectionOut[i].SequenceEqual(wdSSW))
                    WindDirectionIn.Add("SSW");
                if (WindDirectionOut[i].SequenceEqual(wdSW))
                    WindDirectionIn.Add("SW");
                if (WindDirectionOut[i].SequenceEqual(wdWSW))
                    WindDirectionIn.Add("WSW");
                if (WindDirectionOut[i].SequenceEqual(wdW))
                    WindDirectionIn.Add("W");
                if (WindDirectionOut[i].SequenceEqual(wdWNW))
                    WindDirectionIn.Add("WNW");
                if (WindDirectionOut[i].SequenceEqual(wdNW))
                    WindDirectionIn.Add("NW");
                if (WindDirectionOut[i].SequenceEqual(wdNNW))
                    WindDirectionIn.Add("NNW");
                if (WindDirectionOut[i].SequenceEqual(wdC))
                    WindDirectionIn.Add("C");


                WindSpeed[i] = WindSpeed[i] * (max[2] - min[2]) + min[2];
                Cloudy[i] = Cloudy[i] * (max[3] - min[3]) + min[3];
                Visibility[i] = Visibility[i] * (max[4] - min[4]) + min[4];
            }

        }

        public void NormWriteResults()
        {
            for (int i = 0; i < WindSpeed.Count(); i++)
            {
                Console.WriteLine(RegionOut[i]);
                Console.WriteLine(Date[i]);
                Console.WriteLine(Hour[i]);
                Console.WriteLine(Temperature[i]);
                Console.WriteLine(Humidity[i]);
                Console.WriteLine(WindDirectionOut[i]);
                Console.WriteLine(WindSpeed[i]);
                Console.WriteLine(Cloudy[i]);
                Console.WriteLine(Visibility[i]);
            }
        }
        public void DeNormWriteResults()
        {
            for (int i = 0; i < WindSpeed.Count(); i++)
            {
                Console.WriteLine(RegionIn[i]);
                Console.WriteLine(Date[i]);
                Console.WriteLine(Hour[i]);
                Console.WriteLine(Temperature[i]);
                Console.WriteLine(Humidity[i]);
                Console.WriteLine(WindDirectionIn[i]);
                Console.WriteLine(WindSpeed[i]);
                Console.WriteLine(Cloudy[i]);
                Console.WriteLine(Visibility[i]);
            }
        }
    }
}
