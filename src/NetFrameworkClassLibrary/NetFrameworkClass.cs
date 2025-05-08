using System;
using System.Configuration;

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
