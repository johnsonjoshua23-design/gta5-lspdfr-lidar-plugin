# RagePluginHook and LSPDFR setup

This document explains how to configure the project so it can compile against the actual GTA 5 / RagePluginHook / LSPDFR assemblies.

## 1. Install the required modding tools

Make sure you already have:
- GTA 5 installed
- RagePluginHook installed correctly
- LSPDFR installed correctly

## 2. Add the library paths

You should set up environment variables like this in your build environment:

```powershell
$env:RAGEPLUGINHOOK_PATH = "C:\Games\GTA5\RagePluginHook"
$env:LSPDFR_PATH = "C:\Games\GTA5\LSPDFR"
```

The project file expects the following assemblies to exist:
- `RagePluginHook.dll`
- `LSPD First Response.dll`

If your installation path is different, update the environment variables.

## 3. Build the project

```powershell
dotnet build src/LspdfrLidarPlugin/LspdfrLidarPlugin.csproj
```

## 4. If the build fails

Check the following:
- the environment variables are set correctly
- the assembly names match the real files in your installation
- the target framework matches your RagePluginHook / LSPDFR setup
- you're using the correct .NET Framework version for the plugin runtime

## 5. Output DLL placement

Once the plugin compiles, copy the resulting DLL into your RagePluginHook plugins folder so GTA 5 can load it.

## 6. What the project is doing

The code is structured so that:
- RagePluginHook handles the plugin lifecycle
- the scanner class owns the beam-based logic
- the HUD renderer is responsible for visual output
- the mod is still easy to extend into actual LSPDFR police gameplay

## 7. Next integration steps

After this setup is working, the next step is to replace the placeholder obstacle logic with actual GTA world entity detection:
- nearby vehicles
- nearby peds
- direction and distance filtering
- police-specific target classification
