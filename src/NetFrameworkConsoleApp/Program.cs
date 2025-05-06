using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrameworkConsoleApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var netFramework = new NetFrameworkClassLibrary.NetFrameworkClass();
			netFramework.WriteConfigValueToConsole();

			var netStandard = new NetStandard20ClassLibrary.NetStandardClass();
			netStandard.WriteConfigValueToConsole();
		}
	}
}
