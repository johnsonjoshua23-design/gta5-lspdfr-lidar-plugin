using System;
using System.Collections.Generic;

namespace LspdfrLidarPlugin;

public sealed class LidarPlugin
{
    private readonly LidarScanner _scanner;
    private bool _enabled;

    public LidarPlugin()
    {
        var settings = new ScanSettings
        {
            Range = 80f,
            ScanAngle = 120f,
            Resolution = 2f,
            MaxHits = 256
        };

        _scanner = new LidarScanner(settings);
    }

    public bool IsEnabled => _enabled;

    public void Initialize()
    {
        _enabled = true;
    }

    public void Toggle()
    {
        _enabled = !_enabled;
    }

    public IReadOnlyList<ScanHit> Update(float originX, float originY, float originZ, float headingDegrees)
    {
        if (!_enabled)
        {
            return Array.Empty<ScanHit>();
        }

        return _scanner.Scan(originX, originY, originZ, headingDegrees);
    }

    public string GetStatusText()
    {
        return _enabled ? "LiDAR scanner enabled" : "LiDAR scanner disabled";
    }
}
