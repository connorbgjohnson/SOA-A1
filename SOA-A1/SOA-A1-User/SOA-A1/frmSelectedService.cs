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
namespace SOA_A1
{
    public partial class frmSelectedService : Form
    {
        public frmSelectedService()
        {
            InitializeComponent();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1024];
            string teamName = txtTeamName.Text;
            string teamID = txtTeamID.Text;
            string message = MessageBuilder.unRegisterTeam(teamName, teamID);
            string ipString = txtRegistryIP.Text;
            string portString = txtRegistryPort.Text;
            string[] response = null;
            bool isOK = false;
            frmConnectTeam frm = new frmConnectTeam();
            IPAddress ip = IPAddress.Parse(ipString);
            int port = int.Parse(portString);
            Socket regSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                regSock.Connect(ip, port);
                TCPHelper.sendMessage(message, regSock);
                string respMessage = TCPHelper.receiveMessage(buffer, regSock);
                response = MessageParser.parseMessage(respMessage);
                isOK = MessageParser.checkOK(response[1]);
                if (isOK == true)
                {
                    this.Hide();
                    frm.Show();
                    regSock.Disconnect(true);
                }
                else if (isOK == false)
                {
                    MessageBox.Show("ERROR CODE: " + response[2] + "\n" + response[3]);
                    regSock.Disconnect(true);
                }
            }
            catch
            {

            }
        }
    }
}
