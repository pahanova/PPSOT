using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPSOT
{
    public class BDTariff
    {
        public int id { get; set; }
        public string name { get; set; }
        public string oper { get; set; }
        public string features { get; set; }
        public int connectionprice { get; set; }
        public int mounthlypay { get; set; }
        public BDTariff ()
        {
            id = 0;
            name = "";
            oper = "";
            features = "";
            connectionprice = 0;
            mounthlypay = 0;
        }
    }
}
