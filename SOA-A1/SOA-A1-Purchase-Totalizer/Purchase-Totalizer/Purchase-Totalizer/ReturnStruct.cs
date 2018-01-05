﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase_Totalizer
{
    public struct ReturnStruct
    {
        public bool Valid;
        public decimal Sub_total_amount;
        public decimal PST_amount;
        public decimal HST_amount;
        public decimal GST_amount;
        public decimal Total_purchase_amount;

        public ReturnStruct(bool valid, decimal sub, decimal pst, decimal hst, decimal gst, decimal total)
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
