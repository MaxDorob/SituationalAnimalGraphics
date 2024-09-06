using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

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
            }
        }
    }
}
