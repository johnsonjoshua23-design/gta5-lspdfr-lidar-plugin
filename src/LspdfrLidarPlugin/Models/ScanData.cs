using System.Collections.Generic;
using System.Numerics;

namespace LspdfrLidarPlugin;

public sealed class ScanSettings
{
    public float Range { get; set; } = 80f;
    public float ScanAngle { get; set; } = 120f;
    public float Resolution { get; set; } = 2f;
    public float OriginHeightOffset { get; set; } = 1.2f;
    public int MaxHits { get; set; } = 256;
    public bool DrawDebugLines { get; set; } = true;
}

public sealed class ScanHit
{
    public float AngleDegrees { get; set; }
    public float Distance { get; set; }
    public bool IsHit { get; set; }
    public Vector3 Origin { get; set; }
    public Vector3 HitPoint { get; set; }
}

public sealed class ScanTarget
{
    public string Label { get; set; } = string.Empty;
    public float Distance { get; set; }
    public Vector3 Position { get; set; }
    public bool IsVehicle { get; set; }
    public bool IsPedestrian { get; set; }
}

public sealed class LidarScanner
{
    private readonly ScanSettings _settings;

    public LidarScanner(ScanSettings settings)
    {
        _settings = settings;
    }

    public IReadOnlyList<ScanHit> Scan(float originX, float originY, float originZ, float headingDegrees)
    {
        var hits = new List<ScanHit>();
        var origin = new Vector3(originX, originY + _settings.OriginHeightOffset, originZ);
        var halfAngle = _settings.ScanAngle / 2f;

        for (var angle = -halfAngle; angle <= halfAngle; angle += _settings.Resolution)
        {
            var direction = GetDirectionVector(headingDegrees + angle);
            var rayEnd = origin + direction * _settings.Range;

            var distance = Vector3.Distance(origin, rayEnd);

            var hit = new ScanHit
            {
                AngleDegrees = angle,
                Distance = distance,
                IsHit = false,
                Origin = origin,
                HitPoint = rayEnd
            };

            hits.Add(hit);
        }

        return hits;
    }

    private static Vector3 GetDirectionVector(float headingDegrees)
    {
        var radians = MathF.PI * headingDegrees / 180f;

        var x = MathF.Cos(radians);
        var y = 0f;
        var z = MathF.Sin(radians);

        return new Vector3(x, y, z);
    }
}
