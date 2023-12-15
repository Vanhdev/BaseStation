using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseStation.Models
{
    public class GPS
    {
        public double Latitude {get; set;}
        public double Longitude { get; set;}
        public double Altitude { get; set;}
        public float Accuracy { get; set;}
        public float Speed { get; set;}
    }
}
