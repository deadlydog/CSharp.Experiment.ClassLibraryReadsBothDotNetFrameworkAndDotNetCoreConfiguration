# C# Experiment - Class library can read .NET Framework and .NET Core config files

## 💬 Description

This repo exists to see if we can have a .NET Standard class library read configuration, whether it is referenced from a .NET Framework or .NET Core application.

## ❓ Why this exists

We have a large .NET Framework web application that we are migrating to .NET Core.
There will be a transition period where we will have both .NET Framework and .NET Core applications running side by side.
The existing code has tons of references to `System.Configuration.ConfigurationManager`.
We want to see if we are able to use the same class libraries in both .NET Framework and .NET Core applications, without needing to change the code.
This repo is a proof of concept to see if we can do that.

## ❤️ Donate to support this project

Buy me an apple cider with whipped cream for providing this repo open source and for free 🙂

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.me/deadlydogDan/5USD)
