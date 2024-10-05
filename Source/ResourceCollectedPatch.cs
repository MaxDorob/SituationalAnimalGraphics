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
            var pawn = (__instance.parent as Pawn);
            var map = pawn.Map;
            pawn.Drawer.renderer.renderTree.SetDirty();
            if (pawn.ageTracker.CurKindLifeStage is SeasonalGraphics seasonalGraphics && seasonalGraphics.TryGetGraphic(Utils.GetSeason(map, Find.TickManager.TicksAbs), GenLocalDate.DayOfSeason(pawn) + 1, pawn.Faction != null, pawn.gender == Gender.Female, pawn.TryGetComp<CompHasGatherableBodyResource>()?.Fullness ?? 1f, out _, out _, out var effecter))
            {
                effecter?.Spawn(pawn.Position, map);
            }
        }
    }
}
