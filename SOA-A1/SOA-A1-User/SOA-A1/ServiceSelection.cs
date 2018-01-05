using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace SOA_A1
{
    public partial class ServiceSelection : Form
    {
        public ServiceSelection()
        {
            InitializeComponent();
        }


        private void cmdSelect_Click(object sender, EventArgs e)
        {
            string ipAddressString = txtHostName.Text;
            string portString = txtPort.Text;
            string teamName = txtTeamName.Text;
            string teamMessage = "";
            IPAddress ip = null;
            bool valid = true;
            int port = 0;
            MessageBuilder msgBuild = new MessageBuilder();
            TcpListener listener = new TcpListener(Dns.GetHostEntry(Dns.GetHostName()).AddressList[1], 3000);
            TcpClient registry = null;
            try
            {
                port = int.Parse(portString);
                ip = IPAddress.Parse(ipAddressString);
            }
            catch(Exception ex)
            {
                valid = false;
                MessageBox.Show(ex.Message);
            }
            if (valid == true)
            {
                try
                {


                    teamMessage = msgBuild.registerTeam(teamName);
                    listener.Start();
                    listener.BeginAcceptTcpClient(TCPHelper.OnClientAccepted, listener);
                    registry = TCPHelper.connectEndPoint(ip, port);
                    TCPHelper.sendMessage(registry, teamMessage);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
