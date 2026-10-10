# Setup guide

## Requirements

- .NET SDK 8.0 or newer
- a code editor such as VS Code or Visual Studio
- access to GTA 5 modding documentation for the next phases
- LSPDFR API knowledge for eventual runtime integration

## Build the project

From the repo root, run:

```bash
dotnet build src/LspdfrLidarPlugin/LspdfrLidarPlugin.csproj
```

## Why this project is intentionally incomplete

This is a learning skeleton. It does not yet include the external GTA 5 and LSPDFR references that would normally be provided by your modding environment.

That is intentional. The goal of Step 1 is to create a clean project foundation before integrating the game-specific code.

## Recommended next actions

1. Open the project in your IDE.
2. Read the scanner and bootstrap classes.
3. Understand the data models.
4. Plan what kind of scan you want to build next.

Once the project is stable, we can add game-specific integration and UI behavior.
