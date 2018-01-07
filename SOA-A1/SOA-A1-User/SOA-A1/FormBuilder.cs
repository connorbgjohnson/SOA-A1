using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_A1
{
    class FormBuilder
    {
        public static void buildSelectedService(string[] args, string[] resps, string serviceName, string serviceTeamName, string serviceDescription, string serviceIP, string servicePort, string teamName, string teamID, string expiration, string regIP, string regPort, frmSelectedService frm)
        {
            frm.txtServiceName.Text = serviceName;
            frm.txtServiceTeamName.Text = serviceTeamName;
            frm.txtDescription.Text = serviceDescription;
            frm.txtServiceIP.Text = serviceIP;
            frm.txtServicePort.Text = servicePort;
            frm.txtTeamName.Text = teamName;
            frm.txtTeamID.Text = teamID;
            frm.txtExpiration.Text = expiration;
            frm.txtRegistryIP.Text = regIP;
            frm.txtRegistryPort.Text = regPort;
            frm.Show();
        }

        public static void buildServiceSelection(string teamName, string teamID, string expiration, string regIP, string regPort, frmServiceSelection frm)
        {
            frm.txtTeamName.Text = teamName;
            frm.txtTeamID.Text = teamID;
            frm.txtExpiration.Text = expiration;
            frm.txtRegIP.Text = regIP;
            frm.txtRegPort.Text = regPort;
            frm.Show();
        }
    }
}
