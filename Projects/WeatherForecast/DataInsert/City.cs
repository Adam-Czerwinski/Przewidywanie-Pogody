using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInsert
{
    public enum Regions { C, N, E, S, W }
    public class City
    {

        public int IdCity { get; }
        public string Name { get; }
        public Regions Region { get; }
        public bool IsStation { get; }

        public City(int idCity, string name, Regions region, bool isStation)
        {
            IdCity = idCity;
            Name = name;
            Region = region;
            IsStation = isStation;
        }

        public override string ToString()
        {
            return
                "Id: " + IdCity
                + " Name: " + Name
                + " Region: " + Region
                + " Is station: " + IsStation
                + "\n";
        }

    }
}
