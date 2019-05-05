using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    interface IData
    {
        int Id { get; }
        string Code { get; }
        string Name { get; }
        DateTime Date { get; }
        int Hour { get; }
        double Temperature { get; }
        int Humidity { get; }
        string Direction { get; }
        int Speed { get; }
        int Cloudy { get; }
        int Visibility { get; }
    }
}
