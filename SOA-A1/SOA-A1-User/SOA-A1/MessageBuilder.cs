using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace SOA_A1
{
    class MessageBuilder
    {
        const char BOM = (char)11;
        const char EOS = (char)13;
        const char EOM = (char)28;

        public static string registerTeam(string teamName)
        {
            string message = "";
            message = BOM + "DRC|REG-TEAM|||" + EOS + "INF|" + teamName + "|||" + EOS + EOM + EOS;
            return message;
        }
        public static string unRegisterTeam(string teamName, string teamID)
        {
            string message = "";
            message = BOM + "DRC|UNREG-TEAM|" + teamName + "|" + teamID + "|" + EOS + EOM + EOS;
            return message;
        }
        public static string queryTeam(string teamName, string teamID, string serviceTeamName, string serviceTeamID, string tagName)
        {
            string message = "";
            message = BOM + "DRC|QUERY-TEAM|" + teamName + "|" + teamID + "|" + EOS + "INF|" + serviceTeamName + "|" + serviceTeamID + tagName + EOS + EOM + EOS;
            return message;
        }

        public static string publishService(string teamName, string teamID, string tagName, string serviceName, int securityLevel, int numArgs, int numResponses, string description, List<string> args, List<string> resps, string publishedServerIP, int publishedPort)
        {
            string message = "";
            message = BOM + "DRC|PUB-SERVICE|" + securityLevel + "|" + numArgs.ToString() + "|" + numResponses.ToString() + "|" + description + "|" + EOS;
            foreach (string arg in args)
            {
                message = message + arg;
            }
            message = message + EOS;
            foreach (string resp in resps)
            {
                message = message + resp;
            }
            message = message + EOS + "MCH|" + publishedServerIP + "|" + publishedPort.ToString() + "|" + EOS + EOM + EOS;
            return message;
        }
        public static string queryService(string teamName, string teamID, string tagName)
        {
            string message = "";
            message = BOM + "DRC|QUERY-SERVICE|" + teamName + "|" + teamID + "|" + EOS + "SRV|" + tagName + "||||||" + EOS + EOM + EOS;
            return message;
        }

        public static string executeService(string teamName, string teamID, string serviceName, int numArgs, List<string> args)
        {
            string message = "";
            message = BOM + "DRC|EXEC-SERVICE|" + teamName + "|" + teamID + "|" + EOS + "SRV||" + serviceName + "||" + numArgs + "|||" + EOS;
            foreach (string arg in args)
            {
                message += arg + EOS;
            }
            message += EOS;
            message += EOM;
            message += EOS;
            return message;
        }

        public static string executeServiceReply(params string[] responses)
        {
            string message = "";
            message = BOM + "PUB|OK|||" + responses.Length + "|" + EOS;
            foreach(string response in responses)
            {
                message += response + EOS;
            }

            return message += EOS + EOM + EOS;
        }

        public static string executeServiceReplyError(int errorCode, string errorMessage)
        {
            return BOM + "PUB|NOT-OK|" + errorCode + "|" + errorMessage + "||" + EOS + EOM + EOS;
        }
        
    }
}
