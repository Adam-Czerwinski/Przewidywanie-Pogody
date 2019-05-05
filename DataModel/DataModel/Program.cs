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
            
            LoadData.Load(dataList);

            foreach(Data data in dataList)
                Console.WriteLine(data.ToString());

            Console.ReadKey();
        }
    }
}
