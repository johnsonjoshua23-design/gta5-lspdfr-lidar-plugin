using System;
using System.Collections.Generic;
using System.Numerics;
using Rage;

namespace LspdfrLidarPlugin.Bootstrapping;

public sealed class LidarPlugin : Plugin
{
    private readonly LidarScanner _scanner;
    private readonly LidarHudRenderer _hudRenderer;

    public LidarPlugin()
    {
        var settings = new ScanSettings
        {
            Range = 80f,
            ScanAngle = 120f,
            Resolution = 2f,
            OriginHeightOffset = 1.2f,
            MaxHits = 256,
            DrawDebugLines = true
        };

        _scanner = new LidarScanner(settings);
        _hudRenderer = new LidarHudRenderer();
    }

    public override void Initialize()
    {
        Logger.Info("LiDAR plugin initialized.");
        GameFiber.StartNew(PluginLoop);
    }

    public override void Finally()
    {
        Logger.Info("LiDAR plugin shutting down.");
    }

    private void PluginLoop()
    {
        while (true)
        {
            GameFiber.Yield();

            var player = Game.LocalPlayer;
            if (player == null)
            {
                continue;
            }

            var position = player.Character.Position;
            var heading = player.Character.Heading;

            var obstaclePositions = new List<Vector3>
            {
                position + new Vector3(15f, 0f, 0f),
                position + new Vector3(20f, 0f, 10f),
                position + new Vector3(25f, 0f, -12f),
                position + new Vector3(35f, 0f, 6f)
            };

            var hits = _scanner.Scan(position.X, position.Y, position.Z, heading, obstaclePositions);
            _hudRenderer.Render(hits);
        }
    }
}
