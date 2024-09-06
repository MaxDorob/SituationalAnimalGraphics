using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Noise;

namespace SituationalAnimalGraphics
{
    [HarmonyPatch(typeof(PawnRenderNode_AnimalPart), nameof(PawnRenderNode_AnimalPart.GraphicFor))]
    internal static class PawnRenderNode_AnimalPart_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> result = instructions.ToList();
            var labelSkipGraphics = (Label)result.First(x => x.opcode == OpCodes.Br_S).operand;
            var toInsert = new List<CodeInstruction>()
            {
                new CodeInstruction(OpCodes.Ldarg_1),
                CodeInstruction.Call(typeof(PawnRenderNode_AnimalPart_Patch), nameof(GetCustomGraphic)),
                new CodeInstruction(OpCodes.Dup),
                new CodeInstruction(OpCodes.Stloc_1),
                new CodeInstruction(OpCodes.Brtrue_S, labelSkipGraphics)
            };
            result.InsertRange(result.IndexOf(result.First(x => x.opcode == OpCodes.Stloc_0)) + 1, toInsert);
            return result;
        }
        public static Graphic GetCustomGraphic(Pawn pawn)
        {
            var ext = pawn.ageTracker.CurKindLifeStage as SeasonalGraphics;
            if (ext != null)
            {
                var map = pawn.Map;
                return ext.GetGraphic(Utils.GetSeason(map, pawn.Dead ? pawn.Corpse?.timeOfDeath ?? Find.TickManager.TicksAbs : Find.TickManager.TicksAbs), pawn.Faction != null, pawn.TryGetComp<CompHasGatherableBodyResource>()?.Fullness ?? 1f)?.Graphic;
            }
            return null;
        }
    }
}
