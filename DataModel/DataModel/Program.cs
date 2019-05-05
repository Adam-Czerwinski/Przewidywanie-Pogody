using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Data> dataList = new List<Data>();
            Normalization norm = new Normalization();
            LoadData.Load(dataList);
            norm.Pobierz(dataList);
            norm.Norm();
            //norm.Pisz();

            foreach (Data data in dataList)
                Console.WriteLine(data.ToString());

            Console.ReadKey();
        }
    }
}
