///Project: SOA-A1-Purchase-Totalizer
///File: PurchaseTotalizer.cs
///Date: 2018/01/04
///Author: Lauchlin Morrison
///Contains PurchaseTotallizer class.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A1_Purchase_Totalizer
{
    /// <summary>
    /// Class for handling Purchase-Totalizer calculations.
    /// </summary>
    static class PurchaseTotalizer
    {
        /// <summary>
        /// Calculate tax values based on province.
        /// </summary>
        /// <param name="locationCode">Which province to use tax logic from.</param>
        /// <param name="value">Purchase amount to calculate.</param>
        /// <returns>A struct containing the price break downs.</returns>
        public static TaxBreakdown Calculate(string locationCode, decimal value)
        {
            bool valid = true;
            decimal pst = 0;
            decimal hst = 0;
            decimal gst = 0;

            if(locationCode == "NL")
            {
                hst = decimal.Multiply(value, 0.13m);
            }
            else if (locationCode == "NS")
            {
                hst = decimal.Multiply(value, 0.15m);
            }
            else if (locationCode == "NB")
            {
                hst = decimal.Multiply(value, 0.13m);
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

            return new TaxBreakdown(valid, 
                Math.Round(value, 2), 
                Math.Round(pst, 2), 
                Math.Round(hst, 2), 
                Math.Round(gst, 2), 
                Math.Round(value + pst + hst + gst, 2));
        }

        /// <summary>
        /// Round too monies. *Not implemented yet.*
        /// </summary>
        /// <param name="value">Value to round to money.</param>
        /// <returns>Rounded money value.</returns>
        private static decimal MoneyRounding(decimal value)
        {
            return value;
        }
    }
}
