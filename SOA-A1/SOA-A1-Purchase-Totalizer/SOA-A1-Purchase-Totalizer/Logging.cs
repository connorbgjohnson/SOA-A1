﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SOA_A1_Purchase_Totalizer
{
	static class Logging
	{
		const string PATH = "purchase_totalizer.log";

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
			message = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss") + " " + message;
			//message = 

			try
			{
				using (FileStream file = File.OpenWrite(PATH))
				{
					Encoding encoding = Encoding.UTF8;
					file.Write(encoding.GetBytes(message), 0, encoding.GetByteCount(message));
				}
			}
			catch(Exception ex)
			{
				//Do nothing. We don't want to kill our service if logging is not working.
			}
		}


	}
}
