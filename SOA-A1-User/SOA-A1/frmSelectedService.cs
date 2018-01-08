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

        private void cmdExecute_Click(object sender, EventArgs e)
        {
            string message = "";
            string teamName = txtTeamName.Text;
            string teamID = txtTeamID.Text;
            string serviceName = txtServiceName.Text;
            string argValue = "";
            string errorString = "";
            //error checks
            bool error = false;
            bool parse = false;
            List<string> args = new List<string>();//All arguments converted from controls to strings
            string arg = "";//an indivudual argument string for storage in args
            Socket service = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);//Socket to be used to connect to service
            int port = 0;//Service Port Number
            int.TryParse(txtServicePort.Text, out port);//Parse Port from form text to int value
            IPAddress ip = IPAddress.Parse(txtServiceIP.Text);
            byte[] buffer = new byte[1024];//buffer for storing messages received from service
            //Cycle through all arguments
            foreach (Argument argument in flpArgs.Controls)
            {
                argValue = argument.txtArgValue.Text;//get user input for argument

                //Check the datatype of the argument and then compare the value to the datatype specified
                if (argument.lblArgDataType.Text == "CHAR")
                {
                    if (char.TryParse(argValue, out char result))
                    {
                        parse = true;
                    }
                }
                else if (argument.lblArgDataType.Text == "SHORT")
                {
                    if (short.TryParse(argValue, out short result))
                    {
                        parse = true;
                    }
                }
                else if (argument.lblArgDataType.Text == "INT")
                {
                    if (int.TryParse(argValue, out int result))
                    {
                        parse = true;
                    }
                }
                else if (argument.lblArgDataType.Text == "LONG")
                {
                    if (long.TryParse(argValue, out long result))
                    {
                        parse = true;
                    }
                }
                else if (argument.lblArgDataType.Text == "FLOAT")
                {
                    if (float.TryParse(argValue, out float result))
                    {
                        parse = true;
                    }
                }
                else if (argument.lblArgDataType.Text == "DOUBLE")
                {
                    if (double.TryParse(argValue, out double result))
                    {
                        parse = true;
                    }
                }
                else if (argument.lblArgDataType.Text == "STRING")
                {
                    parse = true;
                }
                //Check if argument is a mandatory field and if it is filled out respectively
                //If argument is mandatory and is not filled in, show error
                if (argument.lblArgMandatory.Text == "mandatory" && argument.txtArgValue.Text == "")
                {
                    error = true;
                    errorString += "ERROR: Mandatory fields not filled in\n";
                }
                //If the argument is not the required datatype, show error
                if (parse == false)
                {
                    error = true;
                    errorString += "ERROR: Value does not correspond to data type\n";
                }
                //If now error is specified (argument is filled in correctly), add argument to the list of arguments
                if (error == false)
                {
                    arg = "ARG|" + argument.lblArgPosition.Text + "|" + argument.lblArgName.Text + "|" + argument.lblArgDataType.Text + "||" + argument.txtArgValue.Text + "|";
                    args.Add(arg);
                }
                //If there is an error, show message instead of adding to the argList
                else
                {
                    MessageBox.Show(errorString);
                    errorString = "";
                    args.Clear();//Empty the arg List and start over
                }
                parse = false;
            }
            try
            {
                //If there were no errors after all args are compiled, send the message
                if (error == false)
                {
                    message = MessageBuilder.executeService(teamName, teamID, serviceName, args.Count, args);//Build the executeService 
                    service.Connect(ip, port);//Connect to the service's socket
                    TCPHelper.sendMessage(message, service);//Send the execute message to the service
                    TCPHelper.receiveMessage(buffer, service);//Wait for a response from the service
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
