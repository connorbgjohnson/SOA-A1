///Project: SOA-A1-Purchase-Totalizer
///File: Program.cs
///Date: 2018/01/05
///Author: Lauchlin Morrison
///This file holds the majority of the communication through the happy path for the service.

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
        static string config_localIP = null;
        static int? config_localPort = null;

        //Service parameters:
        static int numArgs = 2;
        static int numResponses = 5;
        static List<string> listArgs = new List<string>(new string[] { "ARG|1|ProvinceOrTerritory|string|mandatory||", "ARG|2|PurchaseValue|float|mandatory||"});
        static List<string> listResps = new List<string>(new string[] { "RSP|1|SubTotalAmount|float||", "RSP|2|PSTamount|float||", "RSP|3|HSTamount|float||", "RSP|4|GSTamount|float||", "RSP|5|TotalPurchaseAmount|float||" });
        
        static void Main(string[] args)
        {
            Socket registrySocket = null;
            Socket clientSocket = null;

            //Try and load configuration file.
            if (LoadConfig())
            {
                //Log startup message.
                Logging.LogLine("=======================================================");
                Logging.LogLine(string.Format("Team\t: {0}", config_teamName));
                Logging.LogLine(string.Format("Tag-name: {0}", config_tagName));
                Logging.LogLine("Service: " + config_serviceName);
                Logging.LogLine("=======================================================");
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
                    config_localIP, (int)config_localPort);
                Logging.LogLine("Calling SOA-Registry with message :");
                Logging.LogLine("\t" + message);

                SOA_A1.TCPHelper.sendMessage(message, registrySocket);
                byte[] buffer = new byte[1024];
                string responseMessage = SOA_A1.TCPHelper.receiveMessage(buffer, registrySocket);
                Logging.LogLine("\tResponse from SOA-Registry :");
                Logging.LogLine("\t\t" + responseMessage);
                Logging.LogLine("---");
                registrySocket.Close();

                if(responseMessage.Contains("SOA|NOT-OK|") && !responseMessage.Contains("has already published service"))
                {
                    if(responseMessage.Contains("is not registered"))
                    {
                        Logging.LogLine("The team is not registered.");
                        Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - The team is not registered.");
                    }
                    else
                    {
                        Logging.LogLine("An error occured!");
                        Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - An error occured!");
                    }
                }
                else if(responseMessage.Contains("SOA|OK|") || responseMessage.Contains("has already published service"))
                {
                    if(responseMessage.Contains("has already published service"))
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Service is already published.");
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Service published.");
                    }

                    Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Waiting for client connections:");

                    //Start listening for cient connections.
                    TcpListener listener = new TcpListener((int)config_localPort);
                    listener.Start();

                    while (true)
                    {
                        clientSocket = listener.AcceptSocket(); //blocks
                        Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Client request received.");
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
                                Logging.LogLine("---");

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
                                Console.Write(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Checking team with registry...   ");
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
                                    Console.WriteLine("Team is OK!");

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
                                        Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") +
                                            " - Replying to client:\n\tFrom Client:\n\t\tProvinceOrTerritory=" + clientProvinceCode +
                                            "\n\t\tPurchaseValue=" + clientPurchaseValue +
                                            "\n\tTo Client: SubTotalAmount=" + results.Sub_total_amount +
                                            "\n\t\tPSTamount=" + results.PST_amount +
                                            "\n\t\tHSTamount=" + results.HST_amount +
                                            "\n\t\tGSTamount=" + results.GST_amount +
                                            "\n\t\tTotalPurchaseAmount=" + results.Total_purchase_amount);
                                    }
                                    else
                                    {
                                        string resultsMessage = SOA_A1.MessageBuilder.executeServiceReplyError(-3, "Invalid parameters sent.");
                                        Logging.LogLine("Responding to service request :");
                                        Logging.LogLine("\t" + resultsMessage);
                                        clientSocket.Send(Encoding.ASCII.GetBytes(resultsMessage));
                                        Logging.LogLine("---");
                                        Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Replying to client:\n\tInvalid parameters sent.");
                                    }
                                }
                                else if(queryTeamresponseMessage.Contains("SOA|NOT-OK|"))
                                {
                                    if(queryTeamresponseMessage.Contains("insufficient permissions to run this service"))
                                    {
                                        Console.WriteLine("Insufficient permissions!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("NOT-OK: " + SOA_A1.MessageParser.parseMessage(queryTeamresponseMessage)[3]);
                                    }

                                    //Send error message.
                                    int getErrorNumber = int.Parse(SOA_A1.MessageParser.parseMessage(queryTeamresponseMessage)[2]);
                                    string resultsMessage = SOA_A1.MessageBuilder.executeServiceReplyError(-4, SOA_A1.MessageParser.parseMessage(queryTeamresponseMessage)[3]);
                                    Logging.LogLine("Responding to service request :");
                                    Logging.LogLine("\t" + resultsMessage);
                                    clientSocket.Send(Encoding.ASCII.GetBytes(resultsMessage));
                                    Logging.LogLine("---");
                                }
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Exception: " + ex.Message);
                            }
                        }
                        
                        clientStream.Close();
                        clientSocket.Close();
                    }
                }
                else
                {
                    Logging.LogLine("Unhandled error occured: " + responseMessage);
                    Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Unhandled Error Occured.");
                }
            }
            else
            {
                Logging.LogLine("Error loading config file. Application closing.");
                Console.WriteLine(DateTime.Now.ToString("yyyy-dd-mm hh:mm:ss") + " - Error loading config file.");
            }
        }

        /// <summary>
        /// Loads the configuration file and returns a pass or fail value.
        /// This file will create a default configuration file if one does not exist.
        /// </summary>
        /// <returns>If the config was loaded or not.</returns>
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
                }
            }
            else
            {
                CreateConfig();
            }

            return status;
        }

        /// <summary>
        /// Parse the configuration file and fill in values.
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
                    else if (twoRats[0] == "client_ip")
                    {
                        config_localIP = twoRats[1].Replace("\r", "");
                    }
                }
                else if(twoRats.Length > 2)
                {
                    Logging.LogLine("Error in log file cannot have more than one '=' sign: " + line);
                    break;
                }
            }

            if(CheckAllConfigValuesAreFilledIn())
            {
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Check that all the configuration stuff got added.
        /// </summary>
        /// <returns>Returns pass or fail.</returns>
        static bool CheckAllConfigValuesAreFilledIn()
        {
            bool miaow = false;
            if (config_teamID != null &&
                config_tagName != null &&
                config_teamName != null &&
                config_host_ip != null &&
                config_host_port != null &&
                config_serviceName != null &&
                config_securityLevel != null &&
                config_description != null &&
                config_localPort != null &&
                config_localIP != null)
            {
                miaow = true;
            }

            return miaow;
        }

        /// <summary>
        /// Create a default configuration file.
        /// </summary>
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
client_ip=
client_port=3000";
            File.WriteAllText(CONFIG_FILE_PATH, configFileString);
            Console.WriteLine("Created config file as none existed. Please fill it out.");
        }

    }
}
