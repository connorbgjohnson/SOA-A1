/****************************** Module Header ******************************\
Module Name:  MessageBuilder.cs
Project:      SOA-A1-User
Programmer: Connor Johnson
Date: 1/8/2018
Description: Contains the class MessageBuilder
\***************************************************************************/

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        public static string registerTeam(string teamName)
        {
            string message = "";
            message = BOM + "DRC|REG-TEAM|||" + EOS + "INF|" + teamName + "|||" + EOS + EOM + EOS;
            return message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="teamID"></param>
        public static string unRegisterTeam(string teamName, string teamID)
        {
            string message = "";
            message = BOM + "DRC|UNREG-TEAM|" + teamName + "|" + teamID + "|" + EOS + EOM + EOS;
            return message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="teamID"></param>
        /// <param name="serviceTeamName"></param>
        /// <param name="serviceTeamID"></param>
        /// <param name="tagName"></param>
        public static string queryTeam(string teamName, string teamID, string serviceTeamName, string serviceTeamID, string tagName)
        {
            string message = "";
            message = BOM + "DRC|QUERY-TEAM|" + teamName + "|" + teamID + "|" + EOS + "INF|" + serviceTeamName + "|" + serviceTeamID + "|" + tagName + "|" + EOS + EOM + EOS;
            return message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="teamID"></param>
        /// <param name="tagName"></param>
        /// <param name="serviceName"></param>
        /// <param name="securityLevel"></param>
        /// <param name="numArgs"></param>
        /// <param name="numResponses"></param>
        /// <param name="description"></param>
        /// <param name="args"></param>
        /// <param name="resps"></param>
        /// <param name="publishedServerIP"></param>
        /// <param name="publishedPort"></param>
        public static string publishService(string teamName, string teamID, string tagName, string serviceName, int securityLevel, int numArgs, int numResponses, string description, List<string> args, List<string> resps, string publishedServerIP, int publishedPort)
        {
            string message = "";
            message = BOM + "DRC|PUB-SERVICE|" + teamName + "|" + teamID + "|" + securityLevel + "|" + numArgs.ToString() + "|" + numResponses.ToString() + "|" + description + "|" + EOS;
            message += "SRV|" + tagName + "|" + serviceName + "|" + securityLevel + "|" + numArgs + "|" + numResponses + "|" + description + "|" + EOS;
            foreach (string arg in args)
            {
                message = message + arg + EOS;
            }
            foreach (string resp in resps)
            {
                message = message + resp + EOS;
            }
            message = message + "MCH|" + publishedServerIP + "|" + publishedPort.ToString() + "|" + EOS + EOM + EOS;
            return message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="teamID"></param>
        /// <param name="tagName"></param>
        public static string queryService(string teamName, string teamID, string tagName)
        {
            string message = "";
            message = BOM + "DRC|QUERY-SERVICE|" + teamName + "|" + teamID + "|" + EOS + "SRV|" + tagName + "||||||" + EOS + EOM + EOS;
            return message;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="teamID"></param>
        /// <param name="serviceName"></param>
        /// <param name="numArgs"></param>
        /// <param name="args"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="responses"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        public static string executeServiceReplyError(int errorCode, string errorMessage)
        {
            return BOM + "PUB|NOT-OK|" + errorCode + "|" + errorMessage + "||" + EOS + EOM + EOS;
        }
    }
}
