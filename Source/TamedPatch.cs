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
    [HarmonyLib.HarmonyPatch(typeof(Pawn), nameof(Pawn.SetFaction))]
    internal static class TamedPatch
    {
        static void Postfix(Pawn __instance)
        {
            if (__instance.RaceProps.Animal)
            {
                __instance.Drawer.renderer.renderTree.SetDirty();
                var map = __instance.Map;
                if (__instance.ageTracker.CurKindLifeStage is SeasonalGraphics seasonalGraphics && seasonalGraphics.TryGetGraphic(Utils.GetSeason(map, Find.TickManager.TicksAbs), __instance.Faction != null, __instance.gender == Gender.Female, __instance.TryGetComp<CompHasGatherableBodyResource>()?.Fullness ?? 1f, out _, out _, out var effecter))
                {
                    effecter?.Spawn(__instance.Position, map);
                }
            }
        }
    }
}
