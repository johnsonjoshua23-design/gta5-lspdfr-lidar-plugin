# Architecture guide

This project is intended to model a clean architecture for a GTA 5 / LSPDFR LiDAR plugin.

## Core idea

A LiDAR-like mod is not just a single script. It is made from several responsibilities:

1. Plugin bootstrap
   - starts when the game/mod loads
   - registers keybinds, commands, and lifecycle handlers

2. Scanner logic
   - defines the scan origin
   - computes sweep angles
   - calculates vectors
   - tests for object hits

3. Data models
   - scan settings
   - hit results
   - detected targets

4. UI rendering
   - draw lines, markers, labels, and HUD data

5. Runtime configuration
   - allow the user to tune range, angle, color, and key bindings

## Recommended class layout

```text
src/
  LspdfrLidarPlugin/
    Bootstrap/
      LidarPluginBootstrap.cs
    Scanner/
      LidarScanner.cs
    Models/
      ScanSettings.cs
      ScanHit.cs
      ScanTarget.cs
    UI/
      LidarHudRenderer.cs
    Config/
      PluginConfig.cs
    Commands/
      LidarCommands.cs
```

## Important design principle

Keep game integration separate from the scanner logic.

The scanner should operate on generic math primitives and data structures. The game layer should convert from GTA entities to plugin-friendly data. This separation makes the project easier to learn and debug.

## Minimal lifecycle

```text
Initialize()
  -> register commands
  -> load config
  -> create scanner instance

Update()
  -> read player/vehicle state
  -> run scan if enabled
  -> evaluate target data
  -> render UI

Shutdown()
  -> clear handlers
  -> dispose resources
```

## Step 2: scanner logic

The most important concept in a LiDAR system is the beam.

For each beam in a scan cone:
- compute the direction vector in world space
- measure the closest object that falls in that direction
- store the distance and hit location

This repo now uses a simplified world-obstacle simulation to demonstrate the technique without depending on GTA game APIs.

## Step 2 outcome

At the end of this step, you should understand:
- how a scan cone is built
- how beam directions are calculated
- how a world object becomes a LiDAR hit
- how this concept translates into GTA 5 entity scanning
