using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase_Totalizer
{
    class ReturnStruct
    {
        public double Sub_total_amount;
        public double PST_amount;
        public double HST_amount;
        public double GST_amount;
        public double Total_purchase_amount;

        public ReturnStruct(double sub, double pst, double hst, double gst, double total)
        {
            Sub_total_amount = sub;
            PST_amount = pst;
            HST_amount = hst;
            GST_amount = gst;
            Total_purchase_amount = total;
        }
    }
}
