using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A1
{
    class MessageBuilder
    {
        const char BOM = (char)11;
        const char EOS = (char)13;
        const char EOM = (char)28;

        public string registerTeam(string teamName)
        {
            string message = "";
            message = BOM + "DRC|REG-TEAM|||" + EOS + "INF|" + teamName + "|||" + EOS + EOM + EOS;
            return message;
        }
        public string unRegisterTeam(string teamName, string teamID)
        {
            string message = "";
            message = BOM + "DRC|UNREG-TEAM|" + EOS + teamName + "|" + teamID + "|" + EOS + EOM + EOS;
            return message;
        }
        public string queryTeam(string teamName, string teamID, string serviceTag)
        {
            string message = "";
            return message;
        }

        
    }
}
