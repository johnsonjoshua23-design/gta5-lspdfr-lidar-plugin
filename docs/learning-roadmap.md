# Learning roadmap

This file tracks the progressive plan for the LiDAR project.

## Phase 1 - Foundation
- [x] create repository
- [x] create project skeleton
- [x] define plugin lifecycle concepts
- [x] define scanner and data models
- [x] create setup and architecture docs

## Phase 2 - Scanner logic
- add sweep angles and scan directions
- model hit points and range results
- simulate ray-based detection without GTA-specific dependencies

## Phase 3 - Entity-aware scanning
- map to vehicles, pedestrians, and objects
- add nearest target detection
- support filtering by distance and class

## Phase 4 - UI overlay
- render lines and markers
- show scanner sweep animation
- display target distance and heading

## Phase 5 - Commands and config
- toggle scanner with keybinds
- load config values from a file
- add debug mode and diagnostics

## Phase 6 - LSPDFR integration
- adapt to the real mod environment
- connect to player and vehicle state
- test in GTA 5 with LSPDFR
