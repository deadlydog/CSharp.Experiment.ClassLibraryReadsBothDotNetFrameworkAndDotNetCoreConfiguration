using System;
using System.Configuration;

namespace NetStandard20ClassLibrary
{
	public class NetStandardClass
	{
		public void WriteConfigValueToConsole()
		{
			var configValue = ConfigurationManager.AppSettings["MyConfigKey"];
			Console.WriteLine("Net Standard Config value: " + configValue);

			var connectionStringValue = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
			Console.WriteLine("Net Standard Connection String value: " + connectionStringValue);
		}
	}
}
