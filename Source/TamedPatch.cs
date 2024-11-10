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
                if (__instance.TryGetSeasonalGraphic(out _, out _, out var effecter))
                {
                    effecter?.Spawn(__instance.Position, __instance.Map);
                }
            }
        }
    }
}
