using System;
using System.Collections.Generic;
using System.Linq;
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

    public IReadOnlyList<ScanHit> Scan(
        float originX,
        float originY,
        float originZ,
        float headingDegrees,
        IEnumerable<Vector3>? obstacles = null)
    {
        var hits = new List<ScanHit>();
        var origin = new Vector3(originX, originY + _settings.OriginHeightOffset, originZ);
        var beamDirections = BuildBeamDirections(headingDegrees);
        var worldObstacles = obstacles?.ToList() ?? new List<Vector3>();

        foreach (var beam in beamDirections)
        {
            var beamDirection = beam.Direction;
            var beamEnd = origin + beamDirection * _settings.Range;
            var closestDistance = _settings.Range;
            var closestHit = beamEnd;
            var hasHit = false;

            foreach (var obstacle in worldObstacles)
            {
                var toObstacle = obstacle - origin;
                var obstacleDistance = toObstacle.Length();

                if (obstacleDistance <= 0f || obstacleDistance > _settings.Range)
                {
                    continue;
                }

                var normalizedObstacle = toObstacle / obstacleDistance;
                var normalizedBeam = Vector3.Normalize(beamDirection);
                var alignment = Vector3.Dot(normalizedObstacle, normalizedBeam);

                if (alignment <= 0f)
                {
                    continue;
                }

                var angleDifference = MathAcos(Math.Clamp(alignment, -1f, 1f)) * 180f / MathF.PI;
                if (angleDifference > _settings.Resolution * 1.5f)
                {
                    continue;
                }

                if (obstacleDistance < closestDistance)
                {
                    closestDistance = obstacleDistance;
                    closestHit = obstacle;
                    hasHit = true;
                }
            }

            hits.Add(new ScanHit
            {
                AngleDegrees = beam.Angle,
                Distance = hasHit ? closestDistance : _settings.Range,
                IsHit = hasHit,
                Origin = origin,
                HitPoint = hasHit ? closestHit : beamEnd
            });
        }

        return hits;
    }

    private IEnumerable<(float Angle, Vector3 Direction)> BuildBeamDirections(float headingDegrees)
    {
        var halfAngle = _settings.ScanAngle / 2f;

        for (var angle = -halfAngle; angle <= halfAngle; angle += _settings.Resolution)
        {
            yield return (angle, GetDirectionVector(headingDegrees + angle));
        }
    }

    private static Vector3 GetDirectionVector(float headingDegrees)
    {
        var radians = MathF.PI * headingDegrees / 180f;

        var x = MathF.Cos(radians);
        var y = 0f;
        var z = MathF.Sin(radians);

        return new Vector3(x, y, z);
    }

    private static float MathAcos(float value)
    {
        return MathF.Acos(value);
    }
}
