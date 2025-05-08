# C# Experiment - Class library can read .NET Framework and .NET Core config files

## 💬 Description

This repo exists to see if we can have a .NET Standard class library read configuration, whether it is referenced from a .NET Framework or .NET Core application.

## ❓ Why this exists

We have a large .NET Framework web application that we are migrating to .NET Core.
There will be a transition period where we will have both .NET Framework and .NET Core applications running side by side.
The existing code has tons of references to `System.Configuration.ConfigurationManager`.
We want to see if we are able to use the same class libraries in both .NET Framework and .NET Core applications, without needing to change the code.
We also want the .NET Core application to use an `appsettings.json` file for configuration instead of the .NET Framework web/app.config file, but the .NET Framework application will still need to use the `web`/`app.config` file for configuration.
This means we will have the configuration duplicated in both applications during the transition period (e.g. the same settings in the .NET Framework `web`/`app.config` file and the .NET Core `appsettings.json` file).
This repo is a proof of concept to see if we can do that.

## 📃 Results

By using the [System.Configuration.ConfigurationManager](https://www.nuget.org/packages/system.configuration.configurationmanager/) NuGet package, we can read the configuration files from both .NET Framework and .NET Core applications.

You will need to:

- Add the NuGet package to your .NET Core startup project (e.g. a .NET 8 console app).
- Call the [ConfigurationManagerDataInjector class](/src/Net8ConsoleApp/ConfigurationManagerDataInjector.cs) in your .NET Core startup project to inject the JSON configuration data into the `System.Configuration.ConfigurationManager` class.
  - This essentially writes the `AppSettings` and `ConnectionStrings` sections of the `appsettings.json` file to an \<executable app name\>.exe.config XML file on disk, so that `System.Configuration.ConfigurationManager` can find it and read it.

Optionally:

- Add the NuGet package to your .NET Framework startup project (e.g. a .NET 4.8 console app).
- Add the NuGet package to your class library project (e.g. a .NET Standard 2.0 class library).

By default the .NET Framework project and class library may reference the .NET Framework native System.Configuration assembly.
If you plan to eventually run your app on Linux though, the assembly will not be available, so it's a good idea to swap out the .NET Framework native reference for the explicit NuGet package reference, as shown [in this commit](https://github.com/deadlydog/CSharp.Experiment.ClassLibraryReadsBothDotNetFrameworkAndDotNetCoreConfiguration/commit/5875044801de71470cf3d6841aeedb36c92a4f1a).

## ❤️ Donate to support more experiments like this

Buy me an apple cider with whipped cream for providing this repo open source and for free 🙂

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.me/deadlydogDan/5USD)
