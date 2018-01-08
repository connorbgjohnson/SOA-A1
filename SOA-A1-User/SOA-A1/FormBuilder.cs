﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace SOA_A1
{
    class FormBuilder
    {
        public static void buildSelectedService(string[] args, string[] resps, string serviceName, string serviceTeamName, string serviceDescription, string serviceIP, string servicePort, string teamName, string teamID, string expiration, string regIP, string regPort, frmSelectedService frm)
        {
            //Fill text boxes with data from previous forms
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

            List<Argument> arguments = new List<Argument>();//List of all arguments service needs
            List<Response> responses = new List<Response>();//list of all responses service will be sending

            string[] parsedArg = null;//parsing an argument to its deliminited sections
            string[] parsedResp = null;// parsing a response to its delimited sections

            foreach (string arg in args)//cycle through all args
            {
                parsedArg = MessageParser.parseMessage(arg);//parse the arg into its parts
                arguments.Add(new Argument());
                arguments.Last().lblArgPosition.Text = parsedArg[1];
                arguments.Last().lblArgName.Text = parsedArg[2];
                arguments.Last().lblArgDataType.Text = parsedArg[3];
                arguments.Last().lblArgMandatory.Text = parsedArg[4];
                arguments.Last().Name = arg;
                frm.flpArgs.Controls.Add(arguments.Last());
            }
            
            foreach (string resp in resps)
            {
                parsedResp = MessageParser.parseMessage(resp);
                responses.Add(new Response());
                responses.Last().lblRespPosition.Text = parsedResp[1];
                responses.Last().lblRespName.Text = parsedResp[2];
                responses.Last().lblDataType.Text = parsedResp[3];
                arguments.Last().Name = resp;
                frm.flpResps.Controls.Add(responses.Last());
            }
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
