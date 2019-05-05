using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace DataModel
{
    class LoadData
    {
        static public void Load(List<Data> dataList)
        {
            try
            {
                StreamReader file = new StreamReader("..\\..\\..\\dataTest.txt");

                string data;
                string[] var;
                int index = 1;

                while ((data = file.ReadLine()) != null)
                {
                    var = data.Split('\t');

                    dataList.Add(new Data(
                        index, 
                        var[0], 
                        var[1], 
                        new DateTime(int.Parse(var[2]), int.Parse(var[3]), int.Parse(var[4])),
                        int.Parse(var[5]), 
                        double.Parse(var[6].Replace('.', ',')),
                        int.Parse(var[7]),
                        var[8],
                        int.Parse(var[9]),
                        int.Parse(var[10]),
                        int.Parse(var[11])));

                    index++;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\nError: " + e + '\n');
            }
        }

    }
}
