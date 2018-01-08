using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
namespace SOA_A1
{
    public partial class frmConnectTeam : Form
    {
        public frmConnectTeam()
        {
            InitializeComponent();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1024];
            string ipAddressString = txtHostName.Text;
            string portString = txtPort.Text;
            string teamName = txtTeamName.Text;
            string message = "";
            string teamMessage = "";
            IPAddress ip = null;
            bool valid = true;
            bool isOK = false;
            int port = 0;

            Socket regSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            string[] response = null;
            frmServiceSelection frm = new frmServiceSelection();
            
            
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
                    regSock.Connect(ip, port);
                    //Build register team message
                    teamMessage = MessageBuilder.registerTeam(teamName);
                    TCPHelper.sendMessage(teamMessage, regSock);
                    message = TCPHelper.receiveMessage(buffer, regSock);
                    response = MessageParser.parseMessage(message);
                    isOK = MessageParser.checkOK(response[1]);
                    if (isOK == true)
                    {
                        frm.txtTeamName.Text = teamName;
                        frm.txtTeamID.Text = response[2];
                        frm.txtExpiration.Text = response[3];
                        frm.txtRegIP.Text = ipAddressString;
                        frm.txtRegPort.Text = portString;
                        
                        FormBuilder.buildServiceSelection(teamName, response[2], response[3], ipAddressString, portString, frm);
                        this.Hide();
                        regSock.Disconnect(true);
                    }
                    else if (isOK == false)
                    {
                        MessageBox.Show("ERROR CODE: " + response[2] + "\n" + response[3]);
                        regSock.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
                
            }
        }
    }
}
