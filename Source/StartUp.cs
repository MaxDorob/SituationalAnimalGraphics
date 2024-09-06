using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace SituationalAnimalGraphics
{
    [StaticConstructorOnStartup]
    public static class StartUp
    {
        private static Harmony harmony;
        static StartUp()
        {
            harmony = new Harmony("SituationalAnimalGraphics");
            harmony.PatchAll();
        }
    }
}
