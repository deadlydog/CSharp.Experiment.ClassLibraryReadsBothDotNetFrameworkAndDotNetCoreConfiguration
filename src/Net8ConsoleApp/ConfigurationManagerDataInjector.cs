using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Net8ConsoleApp;

internal static class ConfigurationManagerDataInjector
{
	/// <summary>
	/// Loads and overwrites the appSettings and connectionStrings sections of the XML configuration file with the provided configuration object.
	/// This is done by reading the exe.config XML file, overwriting the appSettings and connectionStrings sections, then writing the changes back to disk.
	/// This allows the System.Configuration.ConfigurationManager to be used to read the appSettings and connectionStrings, and have them exactly match
	/// what was provided in the configuration object. e.g. from an appsettings.json file.
	/// </summary>
	/// <param name="configuration"></param>
	public static void LoadAppSettingsAndConnectionStringsIntoConfigurationManager(IConfigurationRoot configuration)
	{
		var xmlConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

		OverwriteAppSettings(configuration, xmlConfig);
		OverwriteConnectionStrings(configuration, xmlConfig);

		// Overwrite the XML configuration file on disk with the new settings that exactly match the provided configuration object.
		xmlConfig.Save(ConfigurationSaveMode.Full, true);

		System.Configuration.ConfigurationManager.RefreshSection("appSettings");
		System.Configuration.ConfigurationManager.RefreshSection("connectionStrings");
	}

	private static void OverwriteAppSettings(IConfigurationRoot configuration, Configuration xmlConfig)
	{
		var xmlAppSettings = xmlConfig.AppSettings.Settings;

		// Wipe out any existing app settings that may have been added from a previous run.
		// Without this, if we ran the app, then removed a setting from the JSON file and ran the app again, the removed
		// setting would still persist in the XML file, and be loaded into the ConfigurationManager.
		// We want the ConfigurationManager to exactly match what is in the provided configuration object.
		xmlAppSettings.Clear();

		var jsonAppSettingsSectionName = "AppSettings";
		foreach (var kvp in configuration.GetSection(jsonAppSettingsSectionName).AsEnumerable())
		{
			// Strip the section name prefix from the key so the key name matches the XML appSettings key name.
			var key = kvp.Key.Replace($"{jsonAppSettingsSectionName}:", string.Empty, StringComparison.OrdinalIgnoreCase);

			// Do not include the empty section name entry.
			if (string.Equals(key, jsonAppSettingsSectionName, StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}

			// Add/Overwrite the key-value pair in the XML appSettings.
			if (xmlAppSettings[key] == null)
			{
				xmlAppSettings.Add(key, kvp.Value);
			}
			else
			{
				xmlAppSettings[key].Value = kvp.Value;
			}
		}
	}

	private static void OverwriteConnectionStrings(IConfigurationRoot configuration, Configuration xmlConfig)
	{
		var xmlConnectionStrings = xmlConfig.ConnectionStrings.ConnectionStrings;

		// Wipe out any existing connection strings that may have been added from a previous run.
		// Without this, if we ran the app, then removed a connection string from the JSON file and ran the app again, the removed
		// connection string would still persist in the XML file, and be loaded into the ConfigurationManager.
		// We want the ConfigurationManager to exactly match what is in the provided configuration object.
		xmlConnectionStrings.Clear();

		var jsonConnectionStringsSectionName = "ConnectionStrings";
		foreach (var kvp in configuration.GetSection(jsonConnectionStringsSectionName).AsEnumerable())
		{
			// Strip the section name prefix from the key so the key name matches the XML connectionStrings key name.
			var key = kvp.Key.Replace($"{jsonConnectionStringsSectionName}:", string.Empty, StringComparison.OrdinalIgnoreCase);

			// Do not include the empty section name entry.
			if (string.Equals(key, jsonConnectionStringsSectionName, StringComparison.OrdinalIgnoreCase))
			{
				continue;
			}

			// Add/Overwrite the key-value pair in the XML connectionStrings.
			if (xmlConnectionStrings[key] == null)
			{
				xmlConnectionStrings.Add(new ConnectionStringSettings(key, kvp.Value));
			}
			else
			{
				xmlConnectionStrings[key].ConnectionString = kvp.Value;
			}
		}
	}
}
