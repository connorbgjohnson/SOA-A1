using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SOA_A1
{
	static class Logging
	{
		const string PATH = "user.log";

		//Services must log:

		//teamname, tagname, servicename
		//message going out to soa registry as well as responses that come back
		//incoming service requests executed service message contents including:
		//The calling team and their ID
		//the service name, arguments and values
		//The response created to send back.
		//Any and all error conditions and exceptions.(Includeing socket errors.)

		/// <summary>
		/// Write message out to log file.
		/// </summary>
		/// <param name="message">Message to write.</param>
		public static void LogLine(string message)
		{
			//Apppend datetime to the front of the message.
			message = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss") + " " + message.Replace("\0", string.Empty);
			try
			{
				using (StreamWriter file = File.AppendText(PATH))
				{
                    file.WriteLine(message);
				}
			}
			catch(Exception ex)
			{
				//Do nothing. We don't want to kill our service if logging is failing.
			}
		}

	}
}
