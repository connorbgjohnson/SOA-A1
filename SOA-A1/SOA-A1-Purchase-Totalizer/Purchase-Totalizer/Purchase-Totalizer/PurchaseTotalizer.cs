using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase_Totalizer
{
    static class PurchaseTotalizer
    {
        public static ReturnStruct Calculate(string locationCode, decimal value)
        {
            bool valid = false;
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

            }
            else if (locationCode == "QC")
            {

            }
            else if (locationCode == "ON")
            {

            }
            else if (locationCode == "MB")
            {

            }
            else if (locationCode == "SK")
            {

            }
            else if (locationCode == "AB")
            {

            }
            else if (locationCode == "BC")
            {

            }
            else if (locationCode == "YT")
            {

            }
            else if (locationCode == "NT")
            {

            }
            else if (locationCode == "NU")
            {

            }
            else
            {
                valid = false;
                //message = "Location code was incorrect.";
            }

            return new ReturnStruct(valid, value, pst, hst, gst, total);
        }

        public static bool ValidateLocationCode(string locationCode)
        {


            return false;
        }

    }
}
