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
        const string SRV = "SRV";
        public frmServiceSelection()
        {
            InitializeComponent();
        }
        private void cmdExecute_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1024];
            string serviceSelected = cboServiceSelect.Text;
            string teamName = txtTeamName.Text;
            string teamID = txtTeamID.Text;
            string ipString = txtRegIP.Text;
            string portString = txtRegPort.Text;
            string expiration = txtExpiration.Text;
            frmConnectTeam frmConnect = new frmConnectTeam();
            frmSelectedService frmService = new frmSelectedService();
            string[] response = null;
            string[] args = null;
            string[] rsps = null;
            string srvInfo = "";
            string[] srvInfoParsed = null;
            string mchInfo = "";
            string[] mchInfoParsed = null;
            bool isOK = false;
            IPAddress regIP = IPAddress.Parse(ipString);
            int regPort = int.Parse(portString);
            string serviceIPString = "";
            string servicePortString = "";
            string serviceTeamName = "";
            string serviceName = "";
            string numArgsString = "";
            int numArgs = 0;
            string numResponsesString = "";
            int numResponses = 0;
            string serviceDescription = "";
            IPAddress serviceIP = null;
            int servicePort = 0;
            string tag = MessageParser.parseTag(serviceSelected);
            string message = MessageBuilder.queryService(teamName, teamID, tag);

            Socket regSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Socket serviceSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                regSock.Connect(regIP, regPort);
                TCPHelper.sendMessage(message, regSock);
                string respMessage = TCPHelper.receiveMessage(buffer, regSock);
                response = MessageParser.parseMessage(respMessage);
                isOK = MessageParser.checkOK(response[1]);
                if (isOK == true)
                {
                    args = MessageParser.argsParser(MessageParser.parseMessageByEOS(respMessage));
                    rsps = MessageParser.respParser(MessageParser.parseMessageByEOS(respMessage));
                    srvInfo = MessageParser.parseSegment(SRV, MessageParser.parseMessageByEOS(respMessage));
                    srvInfoParsed = MessageParser.parseMessage(srvInfo);
                    mchInfo = MessageParser.parseSegment(MCH, MessageParser.parseMessageByEOS(respMessage));
                    mchInfoParsed = MessageParser.parseMessage(mchInfo);
                    serviceIPString = mchInfoParsed[1];
                    servicePortString = mchInfoParsed[2];
                    serviceIP = IPAddress.Parse(serviceIPString);
                    servicePort = int.Parse(servicePortString);
                    serviceTeamName = srvInfoParsed[1];
                    serviceName = srvInfoParsed[2];
                    numArgsString = srvInfoParsed[4];
                    numArgs = int.Parse(numArgsString);
                    numResponsesString = srvInfoParsed[5];
                    numResponses = int.Parse(numResponsesString);
                    serviceDescription = srvInfoParsed[6];
                    
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
