using Microsoft.Extensions.Configuration;
using System.Configuration;

var configuration = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.Build();

var configSettings = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
var xmlAppSettings = configSettings.AppSettings.Settings;
var jsonAppSettingsSectionName = "AppSettings";
foreach (var kvp in configuration.GetSection(jsonAppSettingsSectionName).AsEnumerable())
{
	var key = kvp.Key.Replace($"{jsonAppSettingsSectionName}:", string.Empty, StringComparison.OrdinalIgnoreCase);
	if (string.Equals(key, jsonAppSettingsSectionName, StringComparison.OrdinalIgnoreCase))
	{
		continue;
	}
	if (xmlAppSettings[key] == null)
	{
		xmlAppSettings.Add(key, kvp.Value);
	}
	else
	{
		xmlAppSettings[key].Value = kvp.Value;
	}
}
var xmlConnectionStrings = configSettings.ConnectionStrings.ConnectionStrings;
var jsonConnectionStringsSectionName = "ConnectionStrings";
foreach (var kvp in configuration.GetSection(jsonConnectionStringsSectionName).AsEnumerable())
{
	var key = kvp.Key.Replace($"{jsonConnectionStringsSectionName}:", string.Empty, StringComparison.OrdinalIgnoreCase);
	if (string.Equals(key, jsonConnectionStringsSectionName, StringComparison.OrdinalIgnoreCase))
	{
		continue;
	}
	if (xmlConnectionStrings[key] == null)
	{
		xmlConnectionStrings.Add(new ConnectionStringSettings(key, kvp.Value));
	}
	else
	{
		xmlConnectionStrings[key].ConnectionString = kvp.Value;
	}
}
configSettings.Save(ConfigurationSaveMode.Full, true);
System.Configuration.ConfigurationManager.RefreshSection("appSettings");
System.Configuration.ConfigurationManager.RefreshSection("connectionStrings");

var netFramework = new NetFrameworkClassLibrary.NetFrameworkClass();
netFramework.WriteConfigValueToConsole();

var netStandard = new NetStandard20ClassLibrary.NetStandardClass();
netStandard.WriteConfigValueToConsole();