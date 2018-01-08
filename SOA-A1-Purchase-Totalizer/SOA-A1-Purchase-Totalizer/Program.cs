using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A1_Purchase_Totalizer
{
    class Program
    {
        const string TAG_NAME = "GIORP-TOTAL";

        static void Main(string[] args)
        {
            TaxBreakdown test = PurchaseTotalizer.Calculate("AB", 899.99m);
            Console.WriteLine("IsValid: {0}\nSub Total: {1}\nPST: {2}\nHST: {3}\nGST: {4}\nTotal: {5}", 
                test.Valid, 
                test.Sub_total_amount, 
                test.PST_amount, 
                test.HST_amount, 
                test.GST_amount, 
                test.Total_purchase_amount);

        }
    }
}
