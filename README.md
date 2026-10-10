# LSPDFR GTA 5 LiDAR Plugin

This repo is now being structured as a real GTA 5 mod project using RagePluginHook (RPH) and the LSPDFR API.

The goal is to learn the real plugin development process step-by-step:
- create a plugin project for GTA V
- integrate with RagePluginHook
- structure the code cleanly for a LSPDFR script
- add a LiDAR-style scanner system
- render debug scan data in-game
- later add gameplay logic and police-specific behavior

## What this project is

This repo is intended to be a learning-first RagePluginHook project for GTA 5 / LSPDFR. It is not a complete game mod yet, because the GTA 5 and LSPDFR assemblies are external dependencies installed on your machine.

## Required dependencies

You need the following on your development machine:
- GTA 5 installed
- RagePluginHook (RPH)
- LSPDFR installed and referenced by your project
- Visual Studio 2022 or VS Code with C# support

## Repository structure

```text
src/
  LspdfrLidarPlugin/
    Bootstrapping/
      LidarPlugin.cs
    Scanner/
      LidarScanner.cs
    Models/
      ScanSettings.cs
      ScanHit.cs
      ScanTarget.cs
    UI/
      LidarHudRenderer.cs
    Utils/
      MathUtility.cs
```

## Quick setup

1. Install RagePluginHook and LSPDFR in your GTA 5 modding setup.
2. Set these environment variables before building:
   - `RAGEPLUGINHOOK_PATH`
   - `LSPDFR_PATH`
3. Restore NuGet packages (if any are added later).
4. Build the plugin project.
5. Copy the output DLL into your RagePluginHook `plugins` directory.

## Example environment variable setup

On Windows, in PowerShell:

```powershell
$env:RAGEPLUGINHOOK_PATH = "C:\Games\GTA5\RagePluginHook"
$env:LSPDFR_PATH = "C:\Games\GTA5\LSPDFR"
```

Then build:

```powershell
dotnet build src/LspdfrLidarPlugin/LspdfrLidarPlugin.csproj
```

## Important learning note

This repo is intentionally modular. We are building the real architecture, not a single monolithic script. That is how serious GTA mods are organized.

## Project phases

### Phase 1 - plugin bootstrap
- create a RagePluginHook entry point
- initialize scanner state
- start the main plugin loop

### Phase 2 - scanner logic
- compute beam directions from heading and angle
- detect targets within range
- return distances and hit points

### Phase 3 - UI and diagnostics
- draw debug scan lines
- display nearest target details
- visualize scanner cone in the game world

### Phase 4 - GTA entity integration
- connect to nearby peds and vehicles
- filter by distance and type
- add police-style scan behavior

### Phase 5 - polish
- config files
- toggle keys
- better target labels
- performance tuning

## Next step

We will now move from the math-only model into a real RagePluginHook plugin structure that follows the actual GTA mod workflow.
