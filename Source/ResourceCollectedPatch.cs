using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Noise;

namespace SituationalAnimalGraphics
{
    [HarmonyLib.HarmonyPatch(typeof(CompHasGatherableBodyResource), nameof(CompHasGatherableBodyResource.Gathered))]
    internal static class ResourceCollectedPatch
    {
        static void Postfix(CompHasGatherableBodyResource __instance)
        {
            var pawn = __instance.parent as Pawn;
            if (pawn.TryGetSeasonalGraphic(out _, out _, out var effecter))
            {
                effecter?.Spawn(pawn.Position, pawn.Map);
            }
        }
    }
}
