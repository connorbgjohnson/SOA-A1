using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A1
{
    class MessageParser
    {
        const char BOM = (char)11;
        const char EOS = (char)13;
        const char EOM = (char)28;
        const string GIORP_TOTAL_TAG = "GIORP-TOTAL";
        const string PAYROLL_TAG = "PAYROLL";
        const string CAR_LOAN_TAG = "CAR-LOAN";
        const string POSTAL_TAG = "POSTAL";
        const string GIORP_TOTAL = "Purchase-Totalizer";
        const string PAYROLL = "Pay-Stub Amount Generator";
        const string CAR_LOAN = "Car-Loan Calculator";
        const string POSTAL = "Canadian Postal-Code Validator";
        const string ARG = "ARG";
        const string RSP = "RSP";
        const string MCH = "MCH";

        public static string[] parseMessage(string message)
        {
            string[] segments = message.Split('|');
            return segments;
        }
        public static string[] parseMessageByEOS(string message)
        {
            string[] segments = message.Split(EOS);
            return segments;
        }
        public static bool checkOK(string status)
        {
            bool OK = false;
            if (status == "OK")
            {
                OK = true;
            }
            else if (status == "NOT-OK")
            {
                OK = false;
            }

            return OK;
        }
        public static string[] argsParser(string[] lines)
        {
            List<string> args = new List<string>();
            string[] segment = null;
            foreach (string line in lines)
            {
                segment = parseMessage(line);
                if (segment[0] == ARG)
                {
                    args.Add(line);
                }
            }
            return args.ToArray();
        }
        public static string[] respParser(string[] lines)
        {
            List<string> rsps = new List<string>();
            string[] segment = null;
            foreach (string line in lines)
            {
                segment = parseMessage(line);
                if (segment[0] == RSP)
                {
                    rsps.Add(line);
                }
            }
            return rsps.ToArray();
        }
        public static string parseTag(string serviceName)
        {
            string tag = "";
            if (serviceName == GIORP_TOTAL)
            {
                tag = GIORP_TOTAL_TAG;
            }
            else if (serviceName == PAYROLL)
            {
                tag = PAYROLL_TAG;
            }
            else if (serviceName == CAR_LOAN)
            {
                tag = CAR_LOAN_TAG;
            }
            else if (serviceName == POSTAL)
            {
                tag = POSTAL_TAG;
            }
            return tag;
        }

        public static string parseSegment(string segmentTag, string[] lines)
        {
            string segment = "";
            
            foreach (string line in lines)
            {
                if (parseMessage(line)[0] == segmentTag)
                {
                    segment = line;
                }
            }
            return segment;
        }
    }
}
