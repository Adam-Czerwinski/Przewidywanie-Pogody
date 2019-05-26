
using DataInsert;

namespace NeuralNetwork
{
    public class Program
    {
        static void Main(string[] args)
        {
            ReadDataFile rd = new ReadDataFile();
            Normalization normalization = new Normalization();
            Denormalization denormalization = new Denormalization();
            rd.LoadData();
            var weatherDatas = rd.WeatherDatas;
            var cities = rd.Cities;

            var x = normalization.Normalize(weatherDatas, cities).ToArray();
            var y = denormalization.Denormalize(x, cities).ToArray();

        }
    }
}
