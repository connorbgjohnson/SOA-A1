using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace SOA_A1_Purchase_Totalizer
{
    class Program
    {
        const string CONFIG_FILE_PATH = "purchase_totalizer.config";

        //From config.
        static string config_teamID = "1219";
        static string config_tagName = "GIORP-TOTAL";
        static string config_teamName = "WesNet";
        static string config_host_ip = "10.113.21.66";
        static int config_host_port = 3128;
        static string config_serviceName = "purchaseTotalizer";
        static int config_securityLevel = 1;
        static string config_description = "description";

        //Service parameters:
        static int numArgs = 2;
        static int numResponses = 5;
        static List<string> listArgs = new List<string>(new string[] { "ARG|1|ProvinceOrTerritory|string|mandatory||", "ARG|2|PurchaseValue|float|mandatory||"});
        static List<string> listResps = new List<string>(new string[] { "RSP|1|Subtotalamount|float||", "RSP|2|PSTamount|float||", "RSP|3|HSTamount|float||", "RSP|4|GSTamount|float||", "RSP|5|TotalpurchaseAmount|float||" });
        static string myIP = "10.113.21.30";
        static int config_localPort = 3000;

        static void Main(string[] args)
        {
            //Try and load configuration file.
            if(LoadConfig())
            {
                //Log startup message.
                Logging.LogLine("=================================================================");
                Logging.LogLine(string.Format("Team\t: {0}", config_teamName));
                Logging.LogLine(string.Format("Tag-name: {0}", config_tagName));
                Logging.LogLine("Service: " + "purchaseTotalizer");
                Logging.LogLine("=================================================================");
                Logging.LogLine("---");

                //Publish service to registry.
                Socket registry = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                registry.Connect(config_host_ip, config_host_port);
                string message = SOA_A1.MessageBuilder.publishService(
                    config_teamName,
                    config_teamID,
                    config_tagName,
                    config_serviceName,
                    config_securityLevel,
                    numArgs,
                    numResponses,
                    config_description,
                    listArgs,
                    listResps,
                    myIP, config_localPort);
                Logging.LogLine("Calling SOA-Registry with message :");
                Logging.LogLine("\t" + message);

                SOA_A1.TCPHelper.sendMessage(message, registry);
                byte[] buffer = new byte[1024];
                string responseMessage = SOA_A1.TCPHelper.receiveMessage(buffer, registry);
                Logging.LogLine("\tResponse from SOA-Registry :");
                Logging.LogLine("\t\t" + responseMessage);

                if(responseMessage.Contains("SOA|NOT-OK|"))
                {
                    Console.WriteLine("An error occured.");
                    Logging.LogLine("!!!An error occured.!!!");
                }
                else
                {
                    Console.WriteLine("");
                }
                Console.WriteLine(responseMessage);

                //Start waiting for messages.


                //Probably do something about ending the app.




                //This is an example.
                //TaxBreakdown test = PurchaseTotalizer.Calculate("AB", 899.99m);
                //Console.WriteLine("IsValid: {0}\nSub Total: {1}\nPST: {2}\nHST: {3}\nGST: {4}\nTotal: {5}",
                //    test.Valid,
                //    test.Sub_total_amount,
                //    test.PST_amount,
                //    test.HST_amount,
                //    test.GST_amount,
                //    test.Total_purchase_amount);


            }
            else
            {
                Console.WriteLine("Error loading config file.");
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
            Console.WriteLine("Created config file as none existed. Please fill it out.");
        }

    }
}
