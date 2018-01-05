using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase_Totalizer
{
    static class PurchaseTotalizer
    {
        public static ReturnStruct Calculate(string locationCode, double value)
        {
            double pst = 0;
            double hst = 0;
            double gst = 0;
            double total = 0;


            return new ReturnStruct(value, pst, hst, gst, total);
        }

        public static bool ValidateLocationCode(string locationCode)
        {


            return false;
        }

    }
}
