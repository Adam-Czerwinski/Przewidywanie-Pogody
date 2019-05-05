using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class Normalization
    {
        
        private List<double> Temperature;
        private List<double> Humidity;
        
        private List<double> Speed;
        private List<double> Cloudy;
        private List<double> Visibility;

        public Normalization()
        {
           
            Temperature = new List<double>();
            Humidity = new List<double>();
            Speed = new List<double>();
            Cloudy = new List<double>();
            Visibility = new List<double>();
        }

        public void Pobierz(List<Data> dataList)
        {
            foreach (Data data in dataList)
            {
                Temperature.Add(Convert.ToDouble(data.Temperature));
                Humidity.Add(Convert.ToDouble(data.Humidity));
                Speed.Add(Convert.ToDouble(data.Speed));
                Cloudy.Add(Convert.ToDouble(data.Cloudy));
                Visibility.Add(Convert.ToDouble(data.Visibility));
            }

        }


        public void Norm()
        {

            double[] min = new double[] { -30, 0, 0, 0, 0 };
            double[] max = new double[] { 40, 100, 25, 8, 10 };

           
            for (int i = 0; i < Temperature.Count(); i++)
            {
                Temperature[i] = (Temperature[i] - min[0]) / (max[0] - min[0]);
                Humidity[i] = (Humidity[i] - min[1]) / (max[1] - min[1]);
                Speed[i] = (Speed[i] - min[2]) / (max[2] - min[2]);
                Cloudy[i] = (Cloudy[i] - min[3]) / (max[3] - min[3]);
                Visibility[i] = (Visibility[i] - min[4]) / (max[4] - min[4]);
            }


        }

        public void Pisz()
        {
            for(int i = 0; i < Speed.Count(); i++)
            {
                Console.WriteLine(Temperature[i]);
                Console.WriteLine(Humidity[i]);
                Console.WriteLine(Speed[i]);
                Console.WriteLine(Cloudy[i]);
                Console.WriteLine(Visibility[i]);
            }
        }


    }
}
