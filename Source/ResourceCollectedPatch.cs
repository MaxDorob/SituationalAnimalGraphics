using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    [HarmonyLib.HarmonyPatch(typeof(CompHasGatherableBodyResource), nameof(CompHasGatherableBodyResource.Gathered))]
    internal static class ResourceCollectedPatch
    {
        static void Postfix(CompHasGatherableBodyResource __instance)
        {
            (__instance.parent as Pawn).Drawer.renderer.renderTree.SetDirty();
        }
    }
}
