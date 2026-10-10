using System;
using System.Collections.Generic;
using System.Numerics;
using Rage;

namespace LspdfrLidarPlugin;

public sealed class LidarHudRenderer
{
    public void Render(IReadOnlyList<ScanHit> hits)
    {
        if (hits.Count == 0)
        {
            return;
        }

        foreach (var hit in hits)
        {
            if (!hit.IsHit)
            {
                continue;
            }

            var worldPosition = hit.HitPoint + new Vector3(0f, 0.5f, 0f);
            var drawPosition = worldPosition;

            var marker = new Vector3(
                drawPosition.X,
                drawPosition.Y,
                drawPosition.Z);

            // In a real RagePluginHook mod, this would use Rage.Graphics.DrawMarker or a similar HUD path.
            // This placeholder keeps the architecture clean while we learn the real game integration pattern.
            _ = marker;
        }
    }
}
