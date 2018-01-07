///Project: SOA-A1-Purchase-Totalizer
///File: PurchaseTotalizer.cs
///Date: 2018/01/04
///Author: Lauchlin Morrison
///This file holds the tax breakdown struct.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A1_Purchase_Totalizer
{
    /// <summary>
    /// Struct is the breakdown of the tax calculation.
    /// </summary>
    public struct TaxBreakdown
    {
        public bool Valid;
        public decimal Sub_total_amount;
        public decimal PST_amount;
        public decimal HST_amount;
        public decimal GST_amount;
        public decimal Total_purchase_amount;

        public TaxBreakdown(bool valid, decimal sub, decimal pst, decimal hst, decimal gst, decimal total)
        {
            Valid = valid;
            Sub_total_amount = sub;
            PST_amount = pst;
            HST_amount = hst;
            GST_amount = gst;
            Total_purchase_amount = total;
        }
    }
}
