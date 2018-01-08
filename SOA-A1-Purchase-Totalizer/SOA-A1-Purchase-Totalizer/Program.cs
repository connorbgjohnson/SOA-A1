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
        const char EOM = (char)28;

        //From config.
        static string config_teamID = null;
        static string config_tagName = null;
        static string config_teamName = null;
        static string config_host_ip = null;
        static int? config_host_port = null;
        static string config_serviceName = null;
        static int? config_securityLevel = null;
        static string config_description = null;
        static int? config_localPort = null;

        //Service parameters:
        static int numArgs = 2;
        static int numResponses = 5;
        static List<string> listArgs = new List<string>(new string[] { "ARG|1|ProvinceOrTerritory|string|mandatory||", "ARG|2|PurchaseValue|float|mandatory||"});
        static List<string> listResps = new List<string>(new string[] { "RSP|1|SubTotalAmount|float||", "RSP|2|PSTamount|float||", "RSP|3|HSTamount|float||", "RSP|4|GSTamount|float||", "RSP|5|TotalPurchaseAmount|float||" });
        static string myIP = "10.113.21.30";
        

        static void Main(string[] args)
        {
            Socket registrySocket = null;
            Socket clientSocket = null;

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
                registrySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                registrySocket.Connect(config_host_ip, (int)config_host_port);

                string message = SOA_A1.MessageBuilder.publishService(
                    config_teamName,
                    config_teamID,
                    config_tagName,
                    config_serviceName,
                    (int)config_securityLevel,
                    numArgs,
                    numResponses,
                    config_description,
                    listArgs,
                    listResps,
                    myIP, (int)config_localPort);
                Logging.LogLine("Calling SOA-Registry with message :");
                Logging.LogLine("\t" + message);

                SOA_A1.TCPHelper.sendMessage(message, registrySocket);
                byte[] buffer = new byte[1024];
                string responseMessage = SOA_A1.TCPHelper.receiveMessage(buffer, registrySocket);
                Logging.LogLine("\tResponse from SOA-Registry :");
                Logging.LogLine("\t\t" + responseMessage);
                registrySocket.Close();

                if(responseMessage.Contains("SOA|NOT-OK|") && !responseMessage.Contains("has already published service"))
                {
                    Console.WriteLine("An error occured.");
                    Logging.LogLine("!!!An error occured.!!!");
                }
                else if(responseMessage.Contains("SOA|OK|") || responseMessage.Contains("has already published service"))
                {
                    if(responseMessage.Contains("has already published service"))
                    {
                        Console.WriteLine("Service already registered: " + responseMessage);
                    }

                    Console.WriteLine("Waiting for client connections:");

                    //Start listening for cient connections.
                    TcpListener listener = new TcpListener((int)config_localPort);
                    listener.Start();

                    while (true)
                    {
                        clientSocket = listener.AcceptSocket(); //blocks
                        Stream clientStream = new NetworkStream(clientSocket);

                        Logging.LogLine("Receiving service request :");
                        StreamReader sr = new StreamReader(clientStream);
                        StreamWriter sw = new StreamWriter(clientStream);
                        sw.AutoFlush = true;

                        byte[] msgBuffer = new byte[1024];
                        clientSocket.Receive(msgBuffer);

                        string clientMsg = Encoding.ASCII.GetString(msgBuffer);
                        if (clientMsg.Contains("DRC|EXEC-SERVICE|"))
                        {
                            try
                            {
                                Logging.LogLine("\t" + clientMsg);

                                //Parse args and client info.
                                string[] clientArgs = SOA_A1.MessageParser.parseMessage(SOA_A1.MessageParser.parseSegment("DRC", SOA_A1.MessageParser.parseMessageByEOS(clientMsg)));
                                string clientTeamName = clientArgs[2];
                                string clientTeamID = clientArgs[3];
                                string[] msgArgs = SOA_A1.MessageParser.argsParser(SOA_A1.MessageParser.parseMessageByEOS(clientMsg));
                                string clientProvinceCode = SOA_A1.MessageParser.parseMessage(msgArgs[0])[5];
                                decimal clientPurchaseValue = decimal.Parse(SOA_A1.MessageParser.parseMessage(msgArgs[1])[5]);


                                //Communicate to registry to get team info.
                                registrySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                registrySocket.Connect(config_host_ip, (int)config_host_port);
                                string queryTeamMessage = SOA_A1.MessageBuilder.queryTeam(config_teamName, config_teamID, clientTeamName, clientTeamID, config_tagName);
                                Logging.LogLine("Calling SOA-Registry with message :");
                                Logging.LogLine("\t" + queryTeamMessage);
                                SOA_A1.TCPHelper.sendMessage(queryTeamMessage, registrySocket);
                                byte[] queryTeamBuffer = new byte[1024];
                                string queryTeamresponseMessage = SOA_A1.TCPHelper.receiveMessage(queryTeamBuffer, registrySocket);
                                Logging.LogLine("\tResponse from SOA-Registry :");
                                Logging.LogLine("\t\t" + queryTeamresponseMessage);
                                Logging.LogLine("---");
                                registrySocket.Close();

                                if (queryTeamresponseMessage.Contains("SOA|OK|"))
                                {
                                    //Perform calculations and send response message.
                                    TaxBreakdown results = PurchaseTotalizer.Calculate(clientProvinceCode, clientPurchaseValue);
                                    if(results.Valid)
                                    {
                                        string resultsMessage = SOA_A1.MessageBuilder.executeServiceReply(
                                            "RSP|1|SubTotalAmount|float|" + results.Sub_total_amount + "|",
                                            "RSP|2|PSTamount|float|" + results.PST_amount + "|",
                                            "RSP|3|HSTamount|float|" + results.HST_amount +"|",
                                            "RSP|4|GSTamount|float|" + results.GST_amount +"|",
                                            "RSP|5|TotalPurchaseAmount|float|" + results.Total_purchase_amount +"|");
                                        Logging.LogLine("Responding to service request :");
                                        Logging.LogLine("\t" + resultsMessage);
                                        clientSocket.Send(Encoding.ASCII.GetBytes(resultsMessage));
                                        Logging.LogLine("---");
                                    }
                                    else
                                    {
                                        string resultsMessage = SOA_A1.MessageBuilder.executeServiceReplyError(-3, "Invalid parameters sent.");
                                        clientSocket.Send(Encoding.ASCII.GetBytes(resultsMessage));
                                    }
                                    //error -3
                                }
                                else if(responseMessage.Contains("SOA|NOT-OK|"))
                                {
                                    //Send error message.
                                    
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        //}
                        
                        clientStream.Close();
                        clientSocket.Close();
                    }
                }
                else
                {
                    Console.WriteLine("Unhandled Error Occured.");
                }
                

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
                using (FileStream fileStream = File.OpenRead(CONFIG_FILE_PATH))
                {
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    string content = "";

                    while (fileStream.Read(b, 0, b.Length) > 0)
                    {
                        content += temp.GetString(b);
                    }

                    if(ParseConfig(content))
                    {
                        status = true;
                    }
                    else
                    {
                        Console.WriteLine("Unable to parse config file.");
                    }

                    //config_teamID
                    //config_tagName
                    //config_teamName
                    //config_host_ip
                    //config_host_port
                    //config_serviceName
                    //config_securityLevel
                    //config_description
                }
            }
            else
            {
                CreateConfig();
            }

            return status;
        }

        /// <summary>
        /// Parse the configuration file.
        /// </summary>
        /// <param name="configString">Config file string.</param>
        /// <returns>If the file was parsed succesfully.</returns>
        static bool ParseConfig(string configString)
        {
            bool success = false;
            string[] fileByLine = configString.Split('\n');
            foreach (string line in fileByLine)
            {
                string[] twoRats = line.Split('=');
                if (twoRats.Length == 2)
                {
                    if(twoRats[0] == "teamId")
                    {
                        config_teamID = twoRats[1].Replace("\r", "");
                    }
                    else if(twoRats[0] == "tagName")
                    {
                        config_tagName = twoRats[1].Replace("\r", "");
                    }
                    else if (twoRats[0] == "teamName")
                    {
                        config_teamName = twoRats[1].Replace("\r", "");
                    }
                    else if (twoRats[0] == "host_ip")
                    {
                        config_host_ip = twoRats[1].Replace("\r", "");
                    }
                    else if (twoRats[0] == "host_port")
                    {
                        config_host_port = int.Parse(twoRats[1]);
                    }
                    else if (twoRats[0] == "serviceName")
                    {
                        config_serviceName = twoRats[1].Replace("\r", "");
                    }
                    else if (twoRats[0] == "securityLevel")
                    {
                        config_securityLevel = int.Parse(twoRats[1]);
                    }
                    else if (twoRats[0] == "description")
                    {
                        config_description = twoRats[1].Replace("\r", "");
                    }
                    else if (twoRats[0] == "client_port")
                    {
                        config_localPort = int.Parse(twoRats[1]);
                    }
                }
                else if(twoRats.Length > 2)
                {
                    Logging.LogLine("Error in log file cannot have more than one '=' sign: " + line);
                    break;
                }
                else
                {
                    //Do nothing. It's an empty line we don't care about.
                }
            }

            if(CheckAllConfigValuesAreFilledIn())
            {
                success = true;
            }

            return success;
        }

        //Check that all the configuration stuff got added.
        static bool CheckAllConfigValuesAreFilledIn()
        {
            bool miaow = false;
            if (config_teamID != null ||
                config_tagName != null ||
                config_teamName != null ||
                config_host_ip != null ||
                config_host_port != null ||
                config_serviceName != null ||
                config_securityLevel != null ||
                config_description != null ||
                config_localPort != null)
            {
                miaow = true;
            }

            return miaow;
        }

        static void CreateConfig()
        {
            string configFileString = @"teamId=
teamName=WesNet
tagName=GIORP-TOTAL
serviceName=purchaseTotalizer
securityLevel=1
description=This service totals the cost and tax and displays it broken down.
host_ip=
host_port=3128
client_port=3000";
            File.WriteAllText(CONFIG_FILE_PATH, configFileString);
            Console.WriteLine("Created config file as none existed. Please fill it out.");
        }

    }
}
