/****************************** Module Header ******************************\
Module Name:  frmConnectTeam.cs
Project:      SOA-A1-User
Programmer: Connor Johnson
Date: 1/8/2018
Contains the logic for the Form frmConnectTeam.cs

\***************************************************************************/

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
            Logging.LogLine("=======================================================");
            Logging.LogLine("\t\t\t -- USER APP LOG --");
            Logging.LogLine("Team:\t: WesNet (Connor Johnson, Lauchlin Morrison, Kyle Kreutzer, Colin Mills");
            Logging.LogLine("=======================================================");
        }

        private void cmdConnect_Click(object sender, EventArgs e)//Button user clicks when trying to connect to the registry
        {
            byte[] buffer = new byte[1024];//data buffer for storing messages
            string ipAddressString = txtHostName.Text;//pull registry ip address from user input
            string portString = txtPort.Text; //pull registry port from user input
            string teamName = txtTeamName.Text;//pull team name from user input
            string message = "";//string for storing received messages
            string teamMessage = "";//string for storing HL7 register team message
            IPAddress ip = null;
            bool valid = true;
            bool isOK = false;
            int port = 0;

            Socket regSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);//Socket for the registry
            string[] response = null;//message received broken into parsed segments by |
            frmServiceSelection frm = new frmServiceSelection();//Next form as an object for building      
            frmConnectTeam frmConnect = new frmConnectTeam();
            try
            {
                port = int.Parse(portString);//turn port from string to int
                ip = IPAddress.Parse(ipAddressString);//turn ip from string to IPAddress
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
                    regSock.Connect(ip, port);//Connect to the registry
                    //Build register team message
                    teamMessage = MessageBuilder.registerTeam(teamName);//Build the Register Team message
                    Logging.LogLine("Calling SOA-Registry with message :");
                    Logging.LogLine("\t" + teamMessage);
                    TCPHelper.sendMessage(teamMessage, regSock);
                    message = TCPHelper.receiveMessage(buffer, regSock);
                    Logging.LogLine("\tResponse from SOA-Registry :");
                    Logging.LogLine("\t\t" + message);
                    Logging.LogLine("---");

                    response = MessageParser.parseMessage(message);
                    isOK = MessageParser.checkOK(response[1]);
                    //Check if the respnse is an OK or NOT-OK response
                    if (isOK == true)
                    {                        
                        FormBuilder.buildServiceSelection(teamName, response[2], response[3], ipAddressString, portString, frm);//pull out the new form
                        this.Hide();//put this form away
                        regSock.Disconnect(true);
                    }
                    else if (isOK == false)
                    {
                        MessageBox.Show("ERROR CODE: " + response[2] + "\n" + response[3]);
                        regSock.Disconnect(true);
                        if (response[3].Contains("Team Licence Expired"))
                        {
                            frmConnect.Show();
                            this.Hide();
                        }
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
