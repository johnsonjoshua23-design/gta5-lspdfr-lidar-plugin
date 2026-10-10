# LSPDFR GTA 5 LiDAR Plugin

This repository is a learning-focused starter project for building a LiDAR-style plugin for LSPDFR in GTA 5.

The goal of this project is to help you learn the full process of:
- setting up a plugin project
- organizing game-mod code cleanly
- designing a LiDAR/scan system
- building the logic for ray-based detection
- rendering scan data in-game
- tuning runtime configuration

Important: this repo is structured as a clean foundation for learning and experimentation. It does not include the GTA 5 game files or LSPDFR package references, since those are external dependencies from the game/modding ecosystem.

## Project structure

- `src/LspdfrLidarPlugin/` - main C# project skeleton
- `docs/` - setup and architecture guides
- `README.md` - project overview and roadmap

## Learning roadmap

### Step 1: project foundation
We are here. This step gives you:
- repo layout
- plugin lifecycle concept
- scanner data models
- baseline classes for future game integration

### Step 2: scanner logic (current step)
We are building:
- beam directions
- scan angle sweep calculation
- hit detection against sample world obstacles
- approximate LiDAR-style beams that return measured distances

### Step 3: gameplay integration
We will connect the scanner to:
- players and vehicles
- nearby entities
- target filtering
- LSPDFR-relevant use cases

### Step 4: UI rendering
We will add:
- HUD overlays
- debug scan lines
- hit markers
- target labels

### Step 5: configuration and polish
We will finish with:
- config files
- commands
- toggles
- debugging helpers

## Quick start

1. Install the .NET SDK.
2. Open the project in your IDE.
3. Build it to confirm the project skeleton compiles.
4. Start with the `LidarScanner` and `LidarPluginBootstrap` classes.
5. Follow the docs for the next steps.

## Example scan simulation

The scanner can now take a sample list of world obstacle positions and run a simplified LiDAR scan.

This is how the logic works:
- for each beam angle in the scanner cone
- compute a direction vector
- iterate nearby obstacles
- if the obstacle appears in the current beam direction
- record the distance and hit point

This is the same concept used in a real LiDAR or raycast system, just simplified for learning.

## Next milestone

The next step is to connect this scanner logic to entity-aware gameplay and add a user-facing visual layer.

This repo is designed to grow with you as you learn.
