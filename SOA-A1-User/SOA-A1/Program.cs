/****************************** Module Header ******************************\
Module Name:  Program.cs
Project:      SOA-A1-User
Programmer: Connor Johnson
Date: 1/8/2018
Description: The main line for the application to run off of

\***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOA_A1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmConnectTeam());
        }
    }
}
