<div align="center">
  
  [![Commit Activity](https://img.shields.io/github/commit-activity/w/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus)
  [![Commit Activity](https://img.shields.io/github/commit-activity/m/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus)
  [![Commit Activity](https://img.shields.io/github/commit-activity/y/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus)
  [![Commit Activity](https://img.shields.io/github/commit-activity/t/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus)
  
  [![Version](https://img.shields.io/github/v/release/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus/releases/latest)
  [![Release Date](https://img.shields.io/github/release-date/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus/releases/latest)
  [![Commits since Latest Release](https://img.shields.io/github/commits-since/Generalisk/SourcePlus/latest)](https://github.com/Generalisk/SourcePlus/releases/latest)
  
  [![License](https://img.shields.io/github/license/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus/blob/main/LICENSE)
  [![Issues](https://img.shields.io/github/issues/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus/issues)
  [![File Size](https://img.shields.io/github/repo-size/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus)
  [![Last Commit](https://img.shields.io/github/last-commit/Generalisk/SourcePlus)](https://github.com/Generalisk/SourcePlus)
  
  [![Source+ Discord](https://img.shields.io/discord/1416533120865406976)](https://discord.gg/pXSKHpuz8K)
</div>

<div align="center">

  [![Windows](https://github.com/Generalisk/SourcePlus/actions/workflows/build-windows.yml/badge.svg)](https://github.com/Generalisk/SourcePlus/actions/workflows/build-windows.yml)
  [![Linux](https://github.com/Generalisk/SourcePlus/actions/workflows/build-linux.yml/badge.svg)](https://github.com/Generalisk/SourcePlus/actions/workflows/build-linux.yml)
</div>

<div align="center">

  # Source+
</div>

Source+ is a custom tool for developing mods in Valve's Source Engine. Source+ takes the iconic Engine and converts it into a Game Engine more in-line with the Engines of today by giving it a custom editor & a multitude of additional tools to help make the development workflow much easier.

## REQUIREMENTS
- [.NET 10.0](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
### Windows (Alternative)
- [Microsoft Visual Studio 2026](https://visualstudio.microsoft.com/downloads/) with the following workflows & components:
  - `.NET desktop development` workflow
  - `.NET 10.0 Runtime`
  - `NuGet package manager`
  - `NuGet targets and build tasks`
<!--I don't know all the components you'll actually need, i'm just guessing :P
Feel free to correct me or add any that i'm missing-->

## BUILD INSTRUCTIONS
To build the project, open the `scripts` folder and click on the appropriate build script.

Inside the folder also lies a set of publish scripts for sharing/distributing without requiring any external dependencies.

Alternatively, you can go to the `src` folder in your operating system's terminal and run the [dotnet build command](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build).
### Using Visual Studio (Windows only)
In Visual Studio, go to the top menu & open the `Build` menu. There, you can pick on whether to build the solution or just the current project.

Alternatively, you can use the `Ctrl + Shift + B` and `Ctrl + B` shortcuts to build the solution and current project respectively.

#### Debugging using Visual Studio
To debug using Visual Studio, go an click on the green arrow with the text `Editor` next to it or press `F5`.

## LICENSE
The Source+ Editor is licensed under the `MIT License`, which you can read [here](LICENSE).

The Source SDK and, by extension, the projects that you create are, by default, licensed under Valve's `SOURCE 1 SDK License`, which can be read [here](https://github.com/ValveSoftware/source-sdk-2013/blob/master/LICENSE).

## USEFUL RESOURCES
- [Valve Developer Wiki](https://developer.valvesoftware.com/wiki/Setting_up_Source_SDK_Base_2013_Multiplayer)
- [Valve Developer Community Discord Server](https://discord.gg/AC8254CJax)
- [Source+ Discord Server](https://discord.gg/pXSKHpuz8K)
