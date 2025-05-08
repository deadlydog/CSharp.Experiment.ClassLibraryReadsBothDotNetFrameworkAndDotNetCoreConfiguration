using Microsoft.Extensions.Configuration;
using Net8ConsoleApp;

var configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

// Inject the settings loaded from the JSON file into the System.Configuration.ConfigurationManager object.
ConfigurationManagerDataInjector.LoadAppSettingsAndConnectionStringsIntoConfigurationManager(configuration);

var netFramework = new NetFrameworkClassLibrary.NetFrameworkClass();
netFramework.WriteConfigValueToConsole();

var netStandard = new NetStandard20ClassLibrary.NetStandardClass();
netStandard.WriteConfigValueToConsole();
