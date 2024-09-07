using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    [HarmonyLib.HarmonyPatch(typeof(ThingComp), nameof(ThingComp.PostSpawnSetup))]
    internal static class CompHasGatherableBodyResource_Patch
    {
        public static void Postfix(ThingComp __instance, bool respawningAfterLoad)
        {
            if (!respawningAfterLoad && __instance is CompHasGatherableBodyResource gatherableBodyResource)
            {
                gatherableBodyResource.fullness = Rand.Range(0.85f, 1f);
            }
        }
    }
}
