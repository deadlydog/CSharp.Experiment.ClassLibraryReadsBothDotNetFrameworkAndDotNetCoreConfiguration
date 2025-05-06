using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrameworkClassLibrary
{
	public class NetFrameworkClass
	{
		public void WriteConfigValueToConsole()
		{
			var configValue = ConfigurationManager.AppSettings["MyConfigKey"];
			Console.WriteLine("Net Framework Config value: " + configValue);

			var connectionStringValue = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
			Console.WriteLine("Net Framework Connection String value: " + connectionStringValue);
		}
	}
}
