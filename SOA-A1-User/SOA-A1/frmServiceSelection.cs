/****************************** Module Header ******************************\
Module Name:  frmServiceSelection.cs
Project:      SOA-A1-User
Programmer: Connor Johnson
Date: 1/8/2018
Description: Contains logic for the Form frmServiceSelection

\***************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace SOA_A1
{
    public partial class frmServiceSelection : Form
    {
        //service tags
        const string GIORP_TOTAL_TAG = "GIORP-TOTAL";
        const string PAYROLL_TAG = "PAYROLL";
        const string CAR_LOAN_TAG = "CAR-LOAN";
        const string POSTAL_TAG = "POSTAL";
        //services
        const string GIORP_TOTAL = "Purchase-Totalizer";
        const string PAYROLL = "Pay-Stub Amount Generator";
        const string CAR_LOAN = "Car-Loan Calculator";
        const string POSTAL = "Canadian Postal-Code Validator";
        //message segment tags
        const string ARG = "ARG";
        const string RSP = "RSP";
        const string MCH = "MCH";
        const string SRV = "SRV";
        public frmServiceSelection()
        {
            InitializeComponent();
        }
        private void cmdSelect_Click(object sender, EventArgs e)//Button for user to confirm selection of a service
        {
            byte[] buffer = new byte[1024];
            string serviceSelected = cboServiceSelect.Text;
            string teamName = txtTeamName.Text;
            string teamID = txtTeamID.Text;
            string ipString = txtRegIP.Text;
            string portString = txtRegPort.Text;
            string expiration = txtExpiration.Text;
            string mchInfo = "";

            frmConnectTeam frmConnect = new frmConnectTeam();
            frmSelectedService frmService = new frmSelectedService();
            string[] response = null;
            string[] args = null;
            string[] rsps = null;
            string srvInfo = "";
            string[] srvInfoParsed = null;
            string[] mchInfoParsed = null;
            bool isOK = false;
            int regPort = int.Parse(portString);
            int numArgs = 0;
            int servicePort = 0;
            int numResponses = 0;
            string serviceIPString = "";
            string servicePortString = "";
            string serviceTeamName = "";
            string serviceName = "";
            string numArgsString = "";
            string serviceDescription = "";
            string numResponsesString = "";
            //Build messages and connect sockets for communication
            string tag = MessageParser.parseTag(serviceSelected);
            string message = MessageBuilder.queryService(teamName, teamID, tag);//Build message to find a service
            IPAddress regIP = IPAddress.Parse(ipString);
            IPAddress serviceIP = null;
            //Declare sockets for registry and service
            Socket regSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Socket serviceSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

            try
            {
                regSock.Connect(regIP, regPort);//Connect to the registry socket
                Logging.LogLine("Calling SOA-Registry with message :");
                Logging.LogLine("\t" + message);
                TCPHelper.sendMessage(message, regSock);//Send query service HL7 /message
                string respMessage = TCPHelper.receiveMessage(buffer, regSock);//Wait for response
                Logging.LogLine("\tResponse from SOA-Registry:");
                Logging.LogLine("\t\t" + respMessage);
                Logging.LogLine("---");
                response = MessageParser.parseMessage(respMessage);//parse response into deliminated parts
                isOK = MessageParser.checkOK(response[1]);//Check if OK or not
                if (isOK == true)
                {
                    args = MessageParser.argsParser(MessageParser.parseMessageByEOS(respMessage));//Get an array of arguments
                    rsps = MessageParser.respParser(MessageParser.parseMessageByEOS(respMessage));//get an array of responses 
                    srvInfo = MessageParser.parseSegment(SRV, MessageParser.parseMessageByEOS(respMessage));//get the SRV segment...
                    srvInfoParsed = MessageParser.parseMessage(srvInfo);//...Parse the SRV segment by | character
                    mchInfo = MessageParser.parseSegment(MCH, MessageParser.parseMessageByEOS(respMessage));//Get the MCH Segment...
                    mchInfoParsed = MessageParser.parseMessage(mchInfo);//...parse the mch segment by | character
                    //Service connection info
                    serviceIPString = mchInfoParsed[1];
                    servicePortString = mchInfoParsed[2];
                    serviceIP = IPAddress.Parse(serviceIPString);
                    servicePort = int.Parse(servicePortString);
                    serviceTeamName = srvInfoParsed[1];
                    serviceName = srvInfoParsed[2];
                    //Parse arguments and responses
                    numArgsString = srvInfoParsed[4];
                    numArgs = int.Parse(numArgsString);
                    numResponsesString = srvInfoParsed[5];
                    numResponses = int.Parse(numResponsesString);
                    serviceDescription = srvInfoParsed[6];
                    //Build the form programmatically 
                    FormBuilder.buildSelectedService(args, rsps, serviceName, serviceTeamName,
                        serviceDescription, serviceIPString, servicePortString,
                        teamName, teamID, expiration, ipString, portString, frmService);
                    this.Hide();
                }
                else if (isOK == false)
                {
                    MessageBox.Show("ERROR CODE: " + response[2] + "\n" + response[3]);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }

        }
        private void cmdDisconnectTeam_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1024];
            string teamName = txtTeamName.Text;
            string teamID = txtTeamID.Text;
            string message = MessageBuilder.unRegisterTeam(teamName, teamID);
            string ipString = txtRegIP.Text;
            string portString = txtRegPort.Text;
            string[] response = null;
            bool isOK = false;
            frmConnectTeam frm = new frmConnectTeam();
            IPAddress ip = IPAddress.Parse(ipString);
            int port = int.Parse(portString);
            Socket regSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                regSock.Connect(ip, port);
                Logging.LogLine("Calling SOA-Registry with message :");
                Logging.LogLine("\t" + message);
                TCPHelper.sendMessage(message, regSock);
                string respMessage = TCPHelper.receiveMessage(buffer, regSock);
                Logging.LogLine("\tResponse from SOA-Registry:");
                Logging.LogLine("\t\t" + respMessage);
                Logging.LogLine("---");
                response = MessageParser.parseMessage(respMessage);
                isOK = MessageParser.checkOK(response[1]);
                if (isOK == true)
                {
                    frm.Show();
                    this.Hide();
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
