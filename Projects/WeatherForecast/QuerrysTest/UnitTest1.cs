using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer;

namespace QuerrysTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            string[][][] temp = StatisticRepository.Get();
            if (temp != null)
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < temp[0][0].Length; i++)
                    {
                        Console.WriteLine(temp[j][0][i] + " : " + temp[j][1][i]);
                    }
                }
        }
    }
}
