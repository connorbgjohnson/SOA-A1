using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A1_Purchase_Totalizer
{
    static class PurchaseTotalizer
    {
        public static ReturnStruct Calculate(string locationCode, decimal value)
        {
            bool valid = true;
            decimal pst = 0;
            decimal hst = 0;
            decimal gst = 0;
            decimal total = 0;

            if(locationCode == "NL")
            {
                hst = decimal.Multiply(value, 0.13m);
                total = value + hst;
            }
            else if (locationCode == "NS")
            {
                hst = decimal.Multiply(value, 0.15m);
                total = value + hst;
            }
            else if (locationCode == "NB")
            {
                hst = decimal.Multiply(value, 0.13m);
                total = value + hst;
            }
            else if (locationCode == "PE")
            {
                gst = decimal.Multiply(value, 0.05m);
                pst = decimal.Multiply(value + gst, 0.10m);
            }
            else if (locationCode == "QC")
            {
                gst = decimal.Multiply(value, 0.05m);
                pst = decimal.Multiply(value + gst, 0.095m);
            }
            else if (locationCode == "ON")
            {
                hst = decimal.Multiply(value, 0.13m);
            }
            else if (locationCode == "MB")
            {
                pst = decimal.Multiply(value, 0.07m);
                gst = decimal.Multiply(value, 0.05m);
            }
            else if (locationCode == "SK")
            {
                pst = decimal.Multiply(value, 0.05m);
                gst = decimal.Multiply(value, 0.05m);
            }
            else if (locationCode == "AB")
            {
                gst = decimal.Multiply(value, 0.05m);
            }
            else if (locationCode == "BC")
            {
                hst = decimal.Multiply(value, 0.12m);
            }
            else if (locationCode == "YT")
            {
                gst = decimal.Multiply(value, 0.05m);
            }
            else if (locationCode == "NT")
            {
                gst = decimal.Multiply(value, 0.05m);
            }
            else if (locationCode == "NU")
            {
                gst = decimal.Multiply(value, 0.05m);
            }
            else
            {
                valid = false;
            }

            return new ReturnStruct(valid, value, pst, hst, gst, total);
        }
    }
}
