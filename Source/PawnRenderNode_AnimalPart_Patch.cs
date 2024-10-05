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
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
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



            labelSkipGraphics = generator.DefineLabel();
            var index = result.IndexOf(result.First(x => x.LoadsField(AccessTools.Field(typeof(PawnKindLifeStage), nameof(PawnKindLifeStage.dessicatedBodyGraphicData)))));
            result[index - 1].opcode = OpCodes.Ldarg_1;
            toInsert = new List<CodeInstruction>()
            {
                CodeInstruction.Call(typeof(PawnRenderNode_AnimalPart_Patch), nameof(GetCustomDessicatedGraphic)),
                new CodeInstruction(OpCodes.Dup),
                new CodeInstruction(OpCodes.Stloc_S, 6),
                new CodeInstruction(OpCodes.Brfalse_S, labelSkipGraphics),
                new CodeInstruction(OpCodes.Ldloc_S, 6),
                new CodeInstruction(OpCodes.Ret),
                new CodeInstruction(OpCodes.Ldloc_0).WithLabels(labelSkipGraphics)
            };
            result.InsertRange(index, toInsert);
            return result;
        }
        public static Graphic GetCustomGraphic(Pawn pawn)
        {
            var ext = pawn.ageTracker.CurKindLifeStage as SeasonalGraphics;
            if (ext != null && ext.TryGetGraphic(Utils.GetSeason(pawn.Map ?? pawn.Corpse?.Map, pawn.Dead ? pawn.Corpse.timeOfDeath + Find.TickManager.gameStartAbsTick : Find.TickManager.TicksAbs), GenLocalDate.DayOfSeason(pawn) + 1, pawn.Faction != null, pawn.gender == Gender.Female, pawn.TryGetComp<CompHasGatherableBodyResource>()?.Fullness ?? 1f, out var result, out _, out _))
            {
                return result?.Graphic;
            }
            return null;
        }
        public static Graphic GetCustomDessicatedGraphic(Pawn pawn)
        {
            var ext = pawn.ageTracker.CurKindLifeStage as SeasonalGraphics;
            if (ext != null && ext.TryGetGraphic(Utils.GetSeason(pawn.Map ?? pawn.Corpse?.Map, pawn.Corpse?.timeOfDeath + Find.TickManager.gameStartAbsTick ?? Find.TickManager.TicksAbs), GenLocalDate.DayOfSeason(pawn) + 1, pawn.Faction != null, pawn.gender == Gender.Female, pawn.TryGetComp<CompHasGatherableBodyResource>()?.Fullness ?? 1f, out _, out var result, out _))
            {
                return result?.GraphicColoredFor(pawn);
            }
            return null;
        }
    }
}
