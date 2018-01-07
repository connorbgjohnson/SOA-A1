using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
namespace SOA_A1
{
    public class TCPHelper
    {
        public static void sendMessage(string message, Socket target)
        {
            target.Send(System.Text.Encoding.ASCII.GetBytes(message));
        }

        public static string receiveMessage(byte[] buffer, Socket target)
        {
            string message = "";
            target.Receive(buffer);
            message = System.Text.Encoding.ASCII.GetString(buffer);
            return message;
        }           
    }
}
