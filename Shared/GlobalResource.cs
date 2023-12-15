using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseStation.Models;

namespace BaseStation.Shared
{
    public class GlobalResource
    {
        public static CellInfor cellInformation { get; set; }
        public static GPS gps { get; set; }
    }
}
