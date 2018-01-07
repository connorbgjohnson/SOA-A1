using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SOA_A1_Purchase_Totalizer
{
    class Program
    {
        const string CONFIG_FILE_PATH = "purchase_totalizer.config";
        const string TAG_NAME = "GIORP-TOTAL";

        //List all the variables needed by the application here. Moving things to config files can be done later.
        //string fasdfasdf

        static void Main(string[] args)
        {
			Logging.LogLine("Welcome to the log file.");


            if(!LoadConfig())
            {
                Console.WriteLine("Error loading config.");
            }
            else
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


        static bool LoadConfig()
        {
            bool status = false;

            if (File.Exists(CONFIG_FILE_PATH))
            {
                status = true;  //REMOVE MEMEMADFAIFHUIGHFAGFUGAUIGFGAS
                using (FileStream fileStream = File.OpenRead(CONFIG_FILE_PATH))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string content = "";

                    while (fileStream.Read(b, 0, b.Length) > 0)
                    {
                        content += temp.GetString(b);
                    }


                }
            }
            else
            {
                CreateConfig();
            }

            return status;
        }


        static void ParseConfig(string configString)
        {
            string[] fileByLine = configString.Split('\n');
            foreach (string line in fileByLine)
            {
                //regex
            }
        }

        static void CreateConfig()
        {
            string configFileString = @"teamname=
teamId=
tagName=
serviceName=
securityLevel=
numArgs=
numResponses=
description=
registryIP=
registryPort=
";
            File.WriteAllText(CONFIG_FILE_PATH, configFileString);
            Console.WriteLine("Created config file as none existed.");
        }

    }
}
