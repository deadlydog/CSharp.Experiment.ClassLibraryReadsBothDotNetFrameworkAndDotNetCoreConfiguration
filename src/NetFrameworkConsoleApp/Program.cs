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
